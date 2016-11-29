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

namespace Murtain.OAuth2.Web.Controllers
{


    public class AccountController : BaseController
    {
        private readonly ICacheManager cacheManager;
        private readonly ICaptchaService captchaService;
        private readonly IUserAccountService userAccountService;

        public AccountController(ICaptchaService messageService, ICacheManager cacheManager, IUserAccountService userAccountService)
        {
            this.captchaService = messageService;
            this.cacheManager = cacheManager;
            this.userAccountService = userAccountService;
        }


        public virtual ActionResult Login(string id) {
            return View();
        }

        public virtual ActionResult Index() {
            return View();
        }
        public virtual ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return View(model);
        }
        public virtual ActionResult LocalRegistration(string signIn)
        {
            return View();
        }
        public virtual ActionResult ValidateCaptcha(ValidateCaptchaViewModel model)
        {
            return View(model);
        }
        public virtual ActionResult SetPassword(SetPasswordViewModel model)
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
        public async Task<ActionResult> GenderatorImageCaptcha()
        {
            return File(await captchaService.GenderatorImageCaptcha(Constants.CookieNames.CaptchaSignup), @"image/jpeg");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResendMessageCode(string telphone, string captcha)
        {
            if (string.IsNullOrEmpty(telphone))
            {
                return Json(new MvcAjaxResponse(false, "无效的手机号码"));
            }

            if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchaSignup)) != captcha)
            {
                return Json(new MvcAjaxResponse(false, "错误的验证码"));
            }

            var response = await captchaService.MessageCaptchaSendAsync(new MessageCaptchaSendRequestModel
            {
                Captcha = MessageCaptcha.Register,
                Telphone = telphone,
                ExpiredTime = 30
            });

            return Json(new MvcAjaxResponse(response.Ok, response.Message));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPasswordAction(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    model.ErrorMessage = LocalizationManager
                        .GetSource(Constants.Localization.SourceName.Views)
                        .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_PASSWORD_NOT_MATCHA);
                    return View("SetPassword", model);
                }
                if (this.cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, MessageCaptcha.Register, model.Telphone, model.Captcha)) != model.Captcha)
                {
                    model.ErrorMessage = LocalizationManager
                        .GetSource(Constants.Localization.SourceName.Views)
                        .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA);
                    return View("SetPassword", model);
                }
                var result = userAccountService.RegisterWithTelphone(new RegisterWithTelphoneRequestModel
                {
                    Telphone = model.Telphone,
                    Password = model.Password
                });
                if (result.Ok)
                {
                    return Redirect("/core/" + IdentityServer3.Core.Constants.RoutePaths.Login + "?signin=" + model.SignIn);
                }
                model.ErrorMessage = result.Message;
            }
            return View("SetPassword", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordAction(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    model.ErrorMessage = LocalizationManager
                        .GetSource(Constants.Localization.SourceName.Views)
                        .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_PASSWORD_NOT_MATCHA);
                    return View("SetPassword", model);
                }
                if (this.cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, MessageCaptcha.Register, model.Telphone, model.Captcha)) != model.Captcha)
                {
                    model.ErrorMessage = LocalizationManager
                        .GetSource(Constants.Localization.SourceName.Views)
                        .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA);
                    return View("SetPassword", model);
                }
                var result = userAccountService.SetPassword(new SetPasswordRequestModel
                {
                    UserId = AppSession.UserId,
                    Password = model.Password
                });
                if (result.Ok)
                {
                    return Redirect("/core/" + IdentityServer3.Core.Constants.RoutePaths.Login + "?signin=" + model.SignIn);
                }
                model.ErrorMessage = result.Message;
            }
            return View("SetPassword", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateCaptchaAction(ValidateCaptchaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, MessageCaptcha.Register, model.Telphone, model.Captcha)) != model.Captcha)
                {
                    model.ErrorMessage = LocalizationManager
                        .GetSource(Constants.Localization.SourceName.Views)
                        .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA);
                    return View("ValidateCaptcha", model);
                }
                return RedirectToAction("SetPassword", new SetPasswordViewModel { Telphone = model.Telphone, Captcha = model.Captcha, SignIn = model.SignIn, Action = model.Action });
            }
            return View("ValidateCaptcha", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LocalRegistrationAction(string signIn, ValidateImageCaptchaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchaSignup)) != model.Captcha)
                {
                    model.ErrorMessage = LocalizationManager
                          .GetSource(Constants.Localization.SourceName.Views)
                          .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_ERROR_CAPTCHA);

                    return View("LocalRegistration");
                }

                var result = await captchaService.MessageCaptchaSendAsync(new MessageCaptchaSendRequestModel
                {
                    Captcha = MessageCaptcha.Register,
                    Telphone = model.Telphone,
                    ExpiredTime = 30
                });

                if (result.Ok)
                {
                    return RedirectToAction("ValidateCaptcha", new ValidateCaptchaViewModel { SignIn = signIn, Captcha = model.Captcha, Telphone = model.Telphone, Action = "SetPasswordAction" });
                }
                model.ErrorMessage = result.Message;
            }
            return View("LocalRegistration", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassowrdAction(string signIn, ValidateImageCaptchaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (CryptoManager.DecryptDES(CookieManager.GetCookieValue(Constants.CookieNames.CaptchaSignup)) != model.Captcha)
                {
                    model.ErrorMessage = LocalizationManager
                          .GetSource(Constants.Localization.SourceName.Views)
                          .GetString(Constants.Localization.MessageIds.USER_ACCOUNT_ERROR_CAPTCHA);

                    return View("ForgotPassword");
                }

                var result = await captchaService.MessageCaptchaSendAsync(new MessageCaptchaSendRequestModel
                {
                    Captcha = MessageCaptcha.RetrievePassword,
                    Telphone = model.Telphone,
                    ExpiredTime = 30
                });

                if (result.Ok)
                {
                    return RedirectToAction("ValidateCaptcha", new ValidateCaptchaViewModel { SignIn = signIn, Captcha = model.Captcha, Telphone = model.Telphone, Action = "ResetPasswordAction" });
                }
                model.ErrorMessage = result.Message;
            }
            return View("LocalRegistration", model);
        }

    }
}