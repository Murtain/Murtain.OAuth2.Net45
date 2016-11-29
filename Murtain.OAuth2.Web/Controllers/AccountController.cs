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
using Murtain.OAuth2.Web.Models;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.AutoMapper;
using Murtain.Runtime.Session;
using Murtain.OAuth2.Application.UserAccount;

namespace Murtain.OAuth2.Web.Controllers
{


    public class AccountController : BaseController
    {
        private readonly IUserAccountService userAccountService;

        public AccountController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        //[Route("core/account/login")]
        //public virtual ActionResult Login(string id) {
        //    return View();
        //}

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
        public virtual ActionResult ValidateCaptcha(ValidateMessageCaptchaViewModel model)
        {
            return View(model);
        }
        public virtual ActionResult SetPassword(LocalRegistrationViewModel model)
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
        [HttpPost]
        public async Task ValidateCaptchaAndResendMessageCaptchaAsync(ValidateGraphicCaptchaAndResendMessageCaptchaViewModel model)
        {
            await userAccountService.ValidateGraphicCaptchaAndResendMessageCaptchaAsync(model.MapTo<ValidateGraphicCaptchaAndResendMessageCaptchaRequestModel>());
        }
        [HttpPost]
        public async Task LocalRegistration(LocalRegistrationViewModel model)
        {
            await userAccountService.LocalRegistrationAsync(model.MapTo<LocalRegistrationRequestModel>());
        }
        [HttpPost]
        public async Task ResetPasswordAsync(ResetPasswordViewModel model)
        {
            await userAccountService.ResetPasswordAsync(model.MapTo<ResetPasswordRequestModel>());
        }
        [HttpPost]
        public async Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaViewModel model)
        {
            await userAccountService.ValidateMessageCaptchaAsync(model.MapTo<ValidateMessageCaptchaRequestModel>());
        }
        [HttpPost]
        public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaViewModel model)
        {
            await userAccountService.ValidateGraphicCaptchaAndSendMessageCaptchaAsync(model.MapTo<ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel>());
        }
    }
}