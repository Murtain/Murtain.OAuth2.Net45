using Murtain.OAuth2.SDK.Captcha;
using Murtain.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Web;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Murtain.OAuth2.SDK.UserAccount
{

    public class ValidateMessageCaptchaRequestModel
    {
        [Required]
        public string Captcha { get; set; }

        [Required]
        public string Mobile { get; set; }
    }

    public enum ValidateMessageCaptcha
    {
        [Description("参数无效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        INVALID_PARAMETERS = SystemReturnCode.INVALID_PARAMETERS,

        [Description("验证码无效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        INVALID_CAPTCHA = 21000,

        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("验证码已过期，请重新获取验证码")]
        EXPIRED_CAPTCHA,

    }
}
