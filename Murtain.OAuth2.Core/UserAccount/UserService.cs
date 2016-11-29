using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using Murtain.Dependency;
using Murtain.Runtime.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static IdentityServer3.Core.Constants;

namespace Murtain.OAuth2.Core.UserAccount
{
    public class UserService : UserServiceBase
    {
        private readonly IUserAccountService userAccountService;
        private readonly OwinContext ctx;

        public UserService(OwinEnvironmentService owinEnv)
        {
            ctx = new OwinContext(owinEnv.Environment);

            userAccountService = IocManager.Instance.Resolve<IUserAccountService>();
        }

        #region [ UserServiceBase method override . ]

        /// <summary>
        /// This method gets called before the login page is shown. This allows you to determine if the user should be authenticated by some out of band mechanism (e.g. client certificates or trusted headers).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            var id = ctx.Request.Query.Get("signin");
            context.AuthenticateResult = new AuthenticateResult("~/Account/Login?id=" + id, (IEnumerable<Claim>)null);
            return Task.FromResult(0);
        }
        /// <summary>
        /// This method gets called for local authentication (whenever the user uses the username and password dialog).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var model = await userAccountService.AuthenticateLocalAsync(context.UserName, context.Password);

            if (model != null)
            {
                context.AuthenticateResult = new AuthenticateResult(model.Subject, model.Telphone);
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
            var claims = context.ExternalIdentity.Claims;

            var user = await userAccountService.AuthenticateExternalAsync(new AuthenticateExternalRequest
            {
                LoginProvider = context.ExternalIdentity.Provider,
                LoginProviderId = context.ExternalIdentity.ProviderId,
                NickName = GetDisplayName(claims),
                Headimageurl = GetUserData(claims)?.Value<string>("HeadimgUrl")
            });

            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject, GetDisplayName(claims), identityProvider: context.ExternalIdentity.Provider);
            }
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
            var subjectId = context.Subject.GetSubjectId();

            var user = await userAccountService.GetProfileDataAsync(subjectId);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Name, user.NickName));
                claims.Add(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Subject, user.Subject));
                claims.Add(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Picture, user.Headimageurl));

                context.IssuedClaims = claims;
            }
        }

        #endregion


        private  string GetDisplayName(IEnumerable<Claim> claims)
        {
            var nameClaim = claims.FirstOrDefault(x => x.Type == IdentityServer3.Core.Constants.ClaimTypes.Name);
            if (nameClaim != null)
            {
                return nameClaim.Value;
            }
            return null;
        }

        private JObject GetUserData(IEnumerable<Claim> claims)
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
