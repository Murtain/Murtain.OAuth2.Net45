using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Http.Owin;
using System.Web.Routing;
using IdentityServer3.Core.ViewModels;
using IdentityServer3.Core.Models;

using Murtain.OAuth2.Core;
using Murtain.OAuth2.Web.Controllers.Shared;
using Murtain.Caching;
using Murtain.Web.Models;
using Murtain.Runtime.Cookie;
using Murtain.Runtime.Security;
using Murtain.Extensions;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.AutoMapper;
using Murtain.Runtime.Session;
using Murtain.OAuth2.Application.UserAccount;

namespace Murtain.OAuth2.Web.Controllers
{

    public class PassportController : LocalizationController
    {
        private readonly IUserAccountService userAccountService;

        public PassportController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

       
        public virtual ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return View(model);
        }



        public virtual ActionResult Logout(LogoutViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult LoggedOut(LoggedOutViewModel model)
        {
            return this.View(model);
        }


        public virtual ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return this.View(model);
        }




        public virtual ActionResult Error(ErrorViewModel model)
        {
            return this.View(model);
        }
    }
}