using Murtain.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Captcha
{
    public enum MessageCaptchaServer
    {
        [Description("短信发送次数超出限制")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        MESSAGES_SENT_OVER_LIMIT = 30000,

        [Description("短信服务不可用")]
        [HttpStatus(HttpStatusCode.BadGateway)]
        SMS_SERVICE_NOT_AVAILABLE,

        [Description("短信发送失败")]
        [HttpStatus(HttpStatusCode.BadGateway)]
        MESSAGE_CAPTCHA_SEND_FAILED,

        [Description("邮件发送失败")]
        [HttpStatus(HttpStatusCode.BadGateway)]
        EMAIL_CAPTCHA_SEND_FAILED,

        [Description("图形验证码无效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        INVALID_GRAPHIC_CAPTCHA,

        [Description("短信验证码已失效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        MESSAGE_CAPTCHA_IS_EXPIRED,

        [Description("短信验证码验证失败")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        MESSAGE_CAPTCHA_NOT_MATCHA,
    }
}
