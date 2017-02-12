using Murtain.OAuth2.SDK.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.Captcha
{
    public class ValidateMessageCaptchaRequest
    {
        /// <summary>
        /// Message captcha
        /// </summary>
        public string MessageCaptcha { get; set; }
        /// <summary>
        /// Message captcha type
        /// </summary>
        [Required]
        public MessageCaptcha MessageCaptchaType { get; set; }
        /// <summary>
        /// Mobile
        /// </summary>
        [Required]
        public string Mobile { get; set; }
    }
}
