using System;
using System.Threading.Tasks;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.OAuth2.Domain.Repositories;
using Murtain.Caching;
using Murtain.Domain.UnitOfWork;
using Murtain.Localization;
using Murtain.Runtime.Security;
using Murtain.AutoMapper;
using Murtain.Exceptions;
using Murtain.Web.Exceptions;

namespace Murtain.OAuth2.Core.UserAccount
{
    public class UserAccountManager : IUserAccountManager
    {
        private readonly IUserAccountRepository userAccountRepository;
        public ILocalizationManager LocalizationManager { get; set; }


        public UserAccountManager(IUserAccountRepository userAccountRepository)
        {
            this.userAccountRepository = userAccountRepository;

            this.LocalizationManager = NullLocalizationManager.Instance;
        }

        public async Task LocalRegistrationAsync(LocalRegistrationRequestModel input)
        {
            if (userAccountRepository.Any(x => x.Mobile == input.Mobile))
            {
                throw new UserFriendlyExceprion(UserAccountManagerServer.USER_ALREADY_EXISTS);
            }

            var entity = new Domain.Entities.UserAccount
            {
                Mobile = input.Mobile,
                Password = _EncryptPassword(input.Password),
                SubId = _Take32Id()
            };

            await userAccountRepository.AddAsync(entity);
        }
        public async Task<Domain.Entities.UserAccount> AuthenticateLocalAsync(string username, string password)
        {
            var entity = await userAccountRepository.FirstOrDefaultAsync(x => x.Mobile == username || x.Email == username);

            if (entity?.Password == _EncryptPassword(password))
            {
                return entity;
            }

            return null;
        }
        public async Task<Domain.Entities.UserAccount> AuthenticateExternalAsync(AuthenticateExternalRequest input)
        {
            var entity = await userAccountRepository.FirstOrDefaultAsync(x => x.LoginProvider == input.LoginProvider && x.LoginProviderId == input.LoginProviderId);

            if (entity == null)
            {
                var u = input.MapTo<Domain.Entities.UserAccount>();

                u.SubId = _Take32Id();

                return await userAccountRepository.AddAsync(u);
            }

            return entity;
        }
        public async Task<Domain.Entities.UserAccount> GetProfileDataAsync(string subId)
        {
            return await userAccountRepository.FirstOrDefaultAsync(x => x.SubId == subId);
        }


        private string _EncryptPassword(string password)
        {
            return CryptoManager.EncryptMD5(password).ToUpper();
        }
        private string _Take32Id()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

    }
}
