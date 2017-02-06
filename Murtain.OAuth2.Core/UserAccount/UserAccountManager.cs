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

namespace Murtain.OAuth2.Core.UserAccount
{
    public class UserAccountManager : IUserAccountManager
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ICacheManager cacheManager;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        public ILocalizationManager LocalizationManager { get; set; }


        public UserAccountManager(IUserAccountRepository userAccountRepository, ICacheManager cacheManager, IUnitOfWorkManager unitOfWorkManager)
        {
            this.userAccountRepository = userAccountRepository;
            this.cacheManager = cacheManager;
            this.unitOfWorkManager = unitOfWorkManager;

            this.LocalizationManager = NullLocalizationManager.Instance;
        }

        //public AddResponseModel Add(AddRequestModel request)
        //{
        //    if (userAccountRepository.Any(x => x.Telphone == request.Telphone))
        //        return new AddResponseModel
        //        {
        //            Ok = false,
        //            Message = LocalizationManager
        //                            .GetSource(Constants.Localization.SourceName.Messages)
        //                            .GetString(Constants.Localization.MessageIds.UserAlreadyExists)
        //        };

        //    var model = request.MapTo<Domain.Entities.UserAccount>();

        //    model.Salt = Guid.NewGuid().ToString().ToUpper();
        //    model.Subject = Guid.NewGuid().ToString().ToUpper();
        //    model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();

        //    userAccountRepository.Add(model);

        //    return new AddResponseModel
        //    {
        //        Ok = true,
        //        Message = LocalizationManager
        //                        .GetSource(Constants.Localization.SourceName.Messages)
        //                        .GetString(Constants.Localization.MessageIds.UserAddComplete)
        //    };
        //}
        //public GetPagingResponseModel GetPaging(GetPagingRequestModel request)
        //{
        //    int total = 0;
        //    var models = userAccountRepository.Get(new EntityFrameworkQuery<Domain.Entities.UserAccount>()
        //                        .Filter(x => x.Name == request.Name)
        //                    )
        //                    .OrderBy(x => request.Sort)
        //                    .Paging(request.PageIndex.TryInt(1), request.PageSize.TryInt(10), out total);

        //    return new GetPagingResponseModel
        //    {
        //        Total = total,
        //        Models = models.MapTo<IList<SDK.UserAccount.UserAccount>>()
        //    };
        //}
        //public GetProfileDataResponseModel GetUserProfileData(GetProfileDataRequestModel request)
        //{
        //    var model = userAccountRepository.FirstOrDefault(x => x.Id == request.Id);
        //    if (model == null)
        //        throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY
        //                    , LocalizationManager
        //                             .GetSource(Constants.Localization.SourceName.Messages)
        //                             .GetString(Constants.Localization.MessageIds.NOT_FIND_BY_PRIMARY_KEY));

        //    return new GetProfileDataResponseModel
        //    {
        //        Model = model.MapTo<SDK.UserAccount.UserAccount>()
        //    };
        //}
        public async Task LocalRegistrationAsync(LocalRegistrationRequestModel input)
        {
            if (userAccountRepository.Any(x => x.Mobile == input.Mobile))
            {
                throw new UserFriendlyExceprion(UserAccountManagerServer.USER_ALREADY_EXISTS);
            }

            var salt = Guid.NewGuid().ToString().ToUpper();

            var entity = new Domain.Entities.UserAccount
            {
                Mobile = input.Mobile,
                Salt = salt,
                Password = CryptoManager.EncryptMD5(input.Password + salt).ToUpper(),
                SubjectId = GeneratorSubjectId()
            };

            await userAccountRepository.InsertAsync(entity);
        }
        //public SaveResponseModel Save(SaveRequestModel request)
        //{
        //    throw new NotImplementedException();
        //}
        //public SetEmailResponseModel SetEmail(SetEmailRequestModel request)
        //{
        //    var model = userAccountRepository.Find(request.Id.TryInt(0));

        //    if (model == null)
        //        throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY, LocalizationManager
        //                            .GetSource(Constants.Localization.SourceName.Messages)
        //                            .GetString(Constants.Localization.MessageIds.NOT_FIND_BY_PRIMARY_KEY));

        //    model.Email = request.Email;
        //    userAccountRepository.UpdateProperty(model, x => new { x.Email });

        //    return new SetEmailResponseModel
        //    {
        //        Ok = true
        //    };
        //}
        //public SetPasswordResponseModel SetPassword(SetPasswordRequestModel request)
        //{
        //    var model = userAccountRepository.FirstOrDefault(x => x.Telphone == request.Telphone);

        //    if (model == null)
        //        throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY, LocalizationManager
        //                            .GetSource(Constants.Localization.SourceName.Messages)
        //                            .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_NOT_EXSIT_MOBILE));

        //    model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();
        //    userAccountRepository.UpdateProperty(model, x => new { x.Password });

        //    return new SetPasswordResponseModel { Ok = true };
        //}



        public Domain.Entities.UserAccount AuthenticateLocalAsync(string username, string password)
        {
            var user = userAccountRepository.FirstOrDefault(x => x.Mobile == username || x.Email == username);

            if (user != null && user.Password == CryptoManager.EncryptMD5(password + user.Salt).ToUpper())
            {
                return user;
            }

            return null;
        }
        public Domain.Entities.UserAccount AuthenticateExternalAsync(AuthenticateExternalRequest input)
        {
            var user = userAccountRepository.FirstOrDefault(x => x.LoginProvider == input.LoginProvider && x.LoginProviderId == input.LoginProviderId);

            if (user == null)
            {
                var entity = input.MapTo<Domain.Entities.UserAccount>();
                entity.SubjectId = GeneratorSubjectId();

                userAccountRepository.Add(entity);
            }

            return user;
        }
        public Domain.Entities.UserAccount GetProfileDataAsync(string subjectId)
        {
            var user = userAccountRepository.FirstOrDefault(x => x.SubjectId == subjectId);

            return user;
        }


        private string GeneratorSubjectId()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}
