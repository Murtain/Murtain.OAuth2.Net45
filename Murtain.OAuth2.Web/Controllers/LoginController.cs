using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Murtain.OAuth2.Application.UserAccount;
using Murtain.OAuth2.SDK.Enum;
using Murtain.SDK.Attributes;
using Murtain.OAuth2.SDK.UserAccount;

namespace Murtain.OAuth2.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAccountService userAccountService;
        public LoginController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        public virtual ActionResult Index()
        {
            return View();
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
        public virtual ActionResult ValidateID()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE type)
        {
            return File(await userAccountService.GetGraphicCaptchaAsync(type), @"image/jpeg");
        }

    }
}