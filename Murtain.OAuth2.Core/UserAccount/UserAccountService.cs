using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.OAuth2.Domain.Repositories;
using Murtain.Caching;
using Murtain.Domain.UnitOfWork;
using Murtain.Localization;
using Murtain.Runtime.Security;
using Murtain.AutoMapper;
using Murtain.Extensions;
using Murtain.Exceptions;
using Murtain.EntityFramework.Queries;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace Murtain.OAuth2.Core.UserAccount
{
    public class UserAccountService : UserServiceBase, IUserAccountService
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ICacheManager cacheManager;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        public ILocalizationManager LocalizationManager { get; set; }

        public UserAccountService(IUserAccountRepository userAccountRepository, ICacheManager cacheManager, IUnitOfWorkManager unitOfWorkManager)
        {
            this.userAccountRepository = userAccountRepository;
            this.cacheManager = cacheManager;
            this.unitOfWorkManager = unitOfWorkManager;

            this.LocalizationManager = NullLocalizationManager.Instance;
        }

        public AddResponseModel Add(AddRequestModel request)
        {
            if (userAccountRepository.Any(x => x.Telphone == request.Telphone))
                return new AddResponseModel
                {
                    Ok = false,
                    Message = LocalizationManager
                                    .GetSource(Constants.Localization.SourceName.Messages)
                                    .GetString(Constants.Localization.MessageIds.UserAlreadyExists)
                };

            var model = request.MapTo<Domain.Entities.UserAccount>();

            model.Salt = Guid.NewGuid().ToString().ToUpper();
            model.Subject = Guid.NewGuid().ToString().ToUpper();
            model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();

            userAccountRepository.Add(model);

            return new AddResponseModel
            {
                Ok = true,
                Message = LocalizationManager
                                .GetSource(Constants.Localization.SourceName.Messages)
                                .GetString(Constants.Localization.MessageIds.UserAddComplete)
            };
        }
        public GetPagingResponseModel GetPaging(GetPagingRequestModel request)
        {
            int total = 0;
            var models = userAccountRepository.Get(new EntityFrameworkQuery<Domain.Entities.UserAccount>()
                                .Filter(x => x.Name == request.Name)
                            )
                            .OrderBy(x => request.Sort)
                            .Paging(request.PageIndex.TryInt(1), request.PageSize.TryInt(10), out total);

            return new GetPagingResponseModel
            {
                Total = total,
                Models = models.MapTo<IList<SDK.UserAccount.UserAccount>>()
            };
        }
        public GetProfileDataResponseModel GetUserProfileData(GetProfileDataRequestModel request)
        {
            var model = userAccountRepository.FirstOrDefault(x => x.Id == request.Id);
            if (model == null)
                throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY
                            , LocalizationManager
                                     .GetSource(Constants.Localization.SourceName.Messages)
                                     .GetString(Constants.Localization.MessageIds.NOT_FIND_BY_PRIMARY_KEY));

            return new GetProfileDataResponseModel
            {
                Model = model.MapTo<SDK.UserAccount.UserAccount>()
            };
        }
        public RegisterWithTelphoneResponseModel RegisterWithTelphone(RegisterWithTelphoneRequestModel request)
        {
            if (userAccountRepository.Any(x => x.Telphone == request.Telphone))
                return new RegisterWithTelphoneResponseModel
                {
                    Ok = false,
                    Message = LocalizationManager
                                    .GetSource(Constants.Localization.SourceName.Messages)
                                    .GetString(Constants.Localization.MessageIds.UserAlreadyExists)
                };

            var model = request.MapTo<Domain.Entities.UserAccount>();

            model.Salt = Guid.NewGuid().ToString().ToUpper();
            model.Subject = Guid.NewGuid().ToString().ToUpper();
            model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();

            userAccountRepository.Add(model);

            return new RegisterWithTelphoneResponseModel
            {
                Ok = true,
                Message = LocalizationManager
                                .GetSource(Constants.Localization.SourceName.Messages)
                                .GetString(Constants.Localization.MessageIds.UserAddComplete)
            };
        }
        public SaveResponseModel Save(SaveRequestModel request)
        {
            throw new NotImplementedException();
        }
        public SetEmailResponseModel SetEmail(SetEmailRequestModel request)
        {
            var model = userAccountRepository.Find(request.Id.TryInt(0));

            if (model == null)
                throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY, LocalizationManager
                                    .GetSource(Constants.Localization.SourceName.Messages)
                                    .GetString(Constants.Localization.MessageIds.NOT_FIND_BY_PRIMARY_KEY));

            model.Email = request.Email;
            userAccountRepository.UpdateProperty(model, x => new { x.Email });

            return new SetEmailResponseModel
            {
                Ok = true
            };
        }
        public SetPasswordResponseModel SetPassword(SetPasswordRequestModel request)
        {
            var model = userAccountRepository.FirstOrDefault(x=>x.Telphone == request.Telphone);

            if (model == null)
                throw new UserFriendlyException((int)Constants.Exception.NOT_FIND_BY_PRIMARY_KEY, LocalizationManager
                                    .GetSource(Constants.Localization.SourceName.Messages)
                                    .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_NOT_EXSIT_MOBILE));

            model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();
            userAccountRepository.UpdateProperty(model, x => new { x.Password });

            return new SetPasswordResponseModel { Ok = true };
        }

        
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                string subject = context.Subject.GetSubjectId();
                var model = userAccountRepository.FirstOrDefault(x => x.Subject == subject);
                if (model != null)
                {
                    var user = model.MapTo<SDK.UserAccount.UserAccount>();

                    user.Claims.Add(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Name, user.NickName ?? "请设置昵称"));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Subject, user.Subject));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Picture, user.Headimageurl ?? ""));

                    context.IssuedClaims = user.Claims;
                }

                return uow.CompleteAsync();
            }
        }
        /// <summary>
        /// 本地登录授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var model = userAccountRepository.FirstOrDefault(x => x.Telphone == context.UserName || x.Email == context.UserName);

                if (model != null && model.Password == CryptoManager.EncryptMD5(context.Password + model.Salt).ToUpper())
                {
                    context.AuthenticateResult = new AuthenticateResult(model.Subject, model.Telphone);
                }

                return uow.CompleteAsync();
            }
        }
        /// <summary>
        /// 外部登录授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var claims = context.ExternalIdentity.Claims;

                var user = userAccountRepository.FirstOrDefault(x => x.LoginProvider == context.ExternalIdentity.Provider && x.LoginProviderId == context.ExternalIdentity.ProviderId);
                if (user == null)
                {
                    var name = context.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == IdentityServer3.Core.Constants.ClaimTypes.Name);
                    var displayName = name == null ? context.ExternalIdentity.ProviderId : name.Value;

                    var userData = GetUserData(claims);

                    user = new Domain.Entities.UserAccount
                    {
                        Subject = Guid.NewGuid().ToString("N").ToUpper(),
                        LoginProvider = context.ExternalIdentity.Provider,
                        LoginProviderId = context.ExternalIdentity.ProviderId,
                        NickName = displayName,
                        Headimageurl = userData.Value<string>("HeadimgUrl")
                    };

                    userAccountRepository.Add(user);
                }
                context.AuthenticateResult = new AuthenticateResult(user.Subject, GetDisplayName(claims), identityProvider: context.ExternalIdentity.Provider);
                return uow.CompleteAsync();
            }
            
        }

        public override Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            return base.PostAuthenticateAsync(context);
        }

        protected virtual string GetDisplayName(IEnumerable<Claim> claims)
        {
            var nameClaim = claims.FirstOrDefault(x => x.Type == IdentityServer3.Core.Constants.ClaimTypes.Name);
            if (nameClaim != null)
            {
                return nameClaim.Value;
            }
            return null;
        }

        protected virtual JObject GetUserData(IEnumerable<Claim> claims)
        {
            var userData = claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.UserData);
            if (userData != null)
            {
                return JObject.Parse(userData.Value);
            }
            return null;
        }
    }
}
