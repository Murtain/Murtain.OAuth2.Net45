using Murtain.OAuth2.SDK.Enum;
using Murtain.SDK;
using Murtain.SDK.Attributes;
using Murtain.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string GraphicCaptcha { get; set; }
        /// <summary>
        /// 验证码类型
        /// </summary>
        [Required]
        public MESSAGE_CAPTCHA_TYPE CaptchaType { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 验证图形验证码
    /// </summary>
    public enum VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE
    {
        /// <summary>
        /// 短信发送次数超出限制
        /// </summary>
        [Description("短信发送次数超出限制")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        MESSAGES_SENT_OVER_LIMIT = RETURN_CODE_SEED.VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE,
        /// <summary>
        /// 短信服务不可用
        /// </summary>
        [Description("短信服务不可用")]
        [HttpCorresponding(HttpStatusCode.BadGateway)]
        SMS_SERVICE_NOT_AVAILABLE,
        /// <summary>
        /// 短信发送失败
        /// </summary>
        [Description("短信发送失败")]
        [HttpCorresponding(HttpStatusCode.BadGateway)]
        MESSAGE_CAPTCHA_SEND_FAILED,
    }

    public class ValidateGraphicCaptchaAndSendMessageCaptchaSample : IJsonSampleModel
    {
        public object GetErrorSampleModel()
        {
            return new ResponseContentModel(VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE.MESSAGES_SENT_OVER_LIMIT, "api/account/sms");
        }

        public object GetRequestSampleModel()
        {
            return new ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel
            {
                CaptchaType = MESSAGE_CAPTCHA_TYPE.REGISTER,
                GraphicCaptcha = "THAG5A",
                Mobile = "15618275259"
            };
        }

        public object GetResponseSampleModel()
        {
            return null;
        }
    }

}
