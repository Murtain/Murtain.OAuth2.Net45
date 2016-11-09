using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using IdentityServer3.Core.Extensions;
using Murtain.Dependency;
using Murtain.Runtime.Session;
using Newtonsoft.Json.Linq;

namespace Murtain.OAuth2.Web.Configuration.Session
{
    public class AppSession : IAppSession, ISingletonDependency
    {
        public virtual string Name
        {
            get
            {
                var claimsIdentity = HttpContext.Current.GetOwinContext().Environment.GetIdentityServerFullLoginAsync()?.Result;
                if (claimsIdentity == null)
                {
                    return null;
                }
                var userNameClaim = claimsIdentity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (userNameClaim == null || string.IsNullOrEmpty(userNameClaim.Value))
                {
                    return null;
                }

                return userNameClaim.Value;
            }
        }
        public virtual string Avatar
        {
            get
            {
                var claimsIdentity = HttpContext.Current.GetOwinContext().Environment.GetIdentityServerFullLoginAsync()?.Result;
                if (claimsIdentity == null)
                {
                    return null;
                }
                var avatar = claimsIdentity.Claims?.FirstOrDefault(c => c.Type == IdentityServer3.Core.Constants.ClaimTypes.Picture);
                if (avatar == null || string.IsNullOrEmpty(avatar.Value))
                {
                    return null;
                }

                return avatar.Value;
            }
        }
        public virtual string UserId
        {
            get
            {
                var claimsIdentity = HttpContext.Current.GetOwinContext().Environment.GetIdentityServerFullLoginAsync()?.Result;
                if (claimsIdentity == null)
                {
                    return null;
                }
                var userIdClaim = claimsIdentity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return null;
                }

                return userIdClaim.Value;
            }
        }
    }
}