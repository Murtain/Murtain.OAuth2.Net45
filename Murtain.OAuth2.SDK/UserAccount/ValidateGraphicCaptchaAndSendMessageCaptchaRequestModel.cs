using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Captcha;
using Murtain.Web;
using Murtain.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    /// <summary>
    /// 验证图形验证码并发送短信验证码
    /// </summary>
    public class ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel
    {
        /// <summary>
        /// 图形验证码
        /// </summary>
        public string GraphicCaptcha { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 验证图形验证码
    /// </summary>
    public enum ValidateGraphicCaptchaAndSendMessageCaptcha
    {
        [Description("参数无效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        INVALID_PARAMETERS = SystemReturnCode.INVALID_PARAMETERS,

        [Description("图形验证码无效")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        INVALID_GRAPHIC_CAPTCHA = MessageCaptchaServer.INVALID_GRAPHIC_CAPTCHA,

        [Description("短信发送次数超出限制")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        MESSAGES_SENT_OVER_LIMIT = MessageCaptchaServer.MESSAGES_SENT_OVER_LIMIT,

        [Description("短信服务不可用")]
        [HttpStatus(HttpStatusCode.BadGateway)]
        SMS_SERVICE_NOT_AVAILABLE = MessageCaptchaServer.SMS_SERVICE_NOT_AVAILABLE,

        [Description("短信发送失败")]
        [HttpStatus(HttpStatusCode.BadGateway)]
        MESSAGE_CAPTCHA_SEND_FAILED = MessageCaptchaServer.MESSAGE_CAPTCHA_SEND_FAILED,
    }



}
