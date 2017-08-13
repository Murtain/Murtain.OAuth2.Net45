using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.Caching;
using Murtain.Domain.UnitOfWork;
using Murtain.Localization;
using Murtain.Runtime.Security;
using Murtain.AutoMapper;
using Murtain.Extensions;
using Murtain.Exceptions;
using Murtain.EntityFramework.Queries;
using Murtain.Dependency;
using Murtain.OAuth2.Core;
using Murtain.OAuth2.Core.UserAccount;
using Murtain.Web.Exceptions;
using Murtain.OAuth2.SDK.Enum;

namespace Murtain.OAuth2.Application.UserAccount
{
    public class UserService : UserServiceBase
    {
        private readonly IUserAccountManager userAccountManager;
        private readonly OwinContext ctx;

        public UserService(OwinEnvironmentService owinEnv, IUserAccountManager userAccountManager)
        {
            ctx = new OwinContext(owinEnv.Environment);

            this.userAccountManager = userAccountManager;
        }

        /// <summary>
        /// This method gets called before the login page is shown. This allows you to determine if the user should be authenticated by some out of band mechanism (e.g. client certificates or trusted headers).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// This method gets called for local authentication (whenever the user uses the username and password dialog).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var model = await userAccountManager.AuthenticateLocalAsync(context.UserName, context.Password);

            if (model != null)
            {
                context.AuthenticateResult = new AuthenticateResult(model.SubId, model.Mobile);
            }
        }
        /// <summary>
        /// This method gets called when the user uses an external identity provider to authenticate.
        /// The user's identity from the external provider is passed via the `externalUser` parameter which contains the
        /// provider identifier, the provider's identifier for the user, and the claims from the provider for the external user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            AuthenticateExternalRequest req = new AuthenticateExternalRequest
            {
                LoginProvider = context.ExternalIdentity.Provider,
                LoginProviderId = context.ExternalIdentity.ProviderId
            };

            switch (context.ExternalIdentity.Provider.TryEmun<OAUTH2_LOGIN_PROVIDER_TYPE>())
            {
                case OAUTH2_LOGIN_PROVIDER_TYPE.WECHAT:
                    req.NickName = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.Name);
                    req.Gender = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.Gender);
                    req.City = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.City);
                    req.Country = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.Country);
                    req.Province = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.Province);
                    req.Avatar = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.Wechat.Avatar);
                    break;
                case OAUTH2_LOGIN_PROVIDER_TYPE.QQ:
                    req.NickName = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.Name);
                    req.Gender = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.Gender);
                    req.City = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.City);
                    req.Country = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.Country);
                    req.Province = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.Province);
                    req.Avatar = _TryValue(context.ExternalIdentity.Claims, Constants.ClaimTypes.qq.Avatar);
                    break;
                case OAUTH2_LOGIN_PROVIDER_TYPE.TAOBAO:
                    break;
                case OAUTH2_LOGIN_PROVIDER_TYPE.BAIDU:
                    break;
                case OAUTH2_LOGIN_PROVIDER_TYPE.ALIPAY:
                    break;
                case OAUTH2_LOGIN_PROVIDER_TYPE.MI:
                    break;
            }

            var user = await userAccountManager.AuthenticateExternalAsync(req);
            if (user == null)
            {
                throw new UserFriendlyException(IDENTITY_SERVER_USER_SERVICE_RETURN_CODE.AUTHENTICATEEXTERNAL_FAILED);
            }

            context.AuthenticateResult = new AuthenticateResult(
                    user.SubId,
                    _TryValue(context.ExternalIdentity.Claims, user.NickName),
                    identityProvider: context.ExternalIdentity.Provider
                    );

        }
        /// <summary>
        /// This method is called prior to the user being issued a login cookie for IdentityServer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            return base.PostAuthenticateAsync(context);
        }
        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var model = await userAccountManager.GetProfileDataAsync(context.Subject.GetSubjectId());

            if (model == null)
            {
                throw new UserFriendlyException(IDENTITY_SERVER_USER_SERVICE_RETURN_CODE.GET_PROFILE_DATA_FAILED);
            }


            context.IssuedClaims = new List<Claim>
            {
                    new Claim(Constants.ClaimTypes.SubId, model.SubId),
                    new Claim(Constants.ClaimTypes.NickName, model.Name),
                    new Claim(Constants.ClaimTypes.Age, model.Age.TryString()),
                    new Claim(Constants.ClaimTypes.Birthday, model.Birthday),
                    new Claim(Constants.ClaimTypes.Gender, model.Gender),
                    new Claim(Constants.ClaimTypes.Avatar, model.Avatar),
                    new Claim(Constants.ClaimTypes.Mobile, model.Mobile),
                    new Claim(Constants.ClaimTypes.Email, model.Email),
                    new Claim(Constants.ClaimTypes.Country, model.Country),
                    new Claim(Constants.ClaimTypes.Province, model.Province),
                    new Claim(Constants.ClaimTypes.City, model.City),
                    new Claim(Constants.ClaimTypes.Address, model.Address)
                };
        }

        private string _TryValue(IEnumerable<Claim> claims, string name)
        {
            return claims.FirstOrDefault(x => x.Type == name)?.Value;
        }
    }
}
