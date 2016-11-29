using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    public class ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel
    {
        public string GraphicCaptcha { get; set; }
        public MessageCaptcha MessageCaptchaType { get; set; }
        public string Mobile { get; set; }
    }
}
