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
using Murtain.OAuth2.SDK.Captcha;
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


    public class AccountController : LocalizationController
    {
        private readonly IUserAccountService userAccountService;

        public AccountController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return View(model);
        }
        public virtual ActionResult LocalRegistration()
        {
            return View();
        }
        public virtual ActionResult ValidateCaptcha()
        {
            return View();
        }
        public virtual ActionResult SetPassword()
        {
            return View();
        }
        public virtual ActionResult Logout(LogoutViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult LoggedOut(LoggedOutViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult ForgotPassword(string signIn)
        {
            return View();
        }
        public virtual ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }
        public virtual ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return this.View(model);
        }
        [Authorize]
        public virtual ActionResult Manager()
        {
            return View();
        }

        public virtual ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return View();
        }

        public virtual ActionResult Error(IdentityServer3.Core.ViewModels.ErrorViewModel model)
        {
            return this.View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetLocalRistrationGraphicCaptcha()
        {
            return File(await userAccountService.GetLocalRistrationGraphicCaptcha(), @"image/jpeg");
        }
        [HttpGet]
        public async Task<ActionResult> GetResetPasswordGraphicCaptcha()
        {
            return File(await userAccountService.GetResetPasswordGraphicCaptcha(), @"image/jpeg");
        }
    }
}