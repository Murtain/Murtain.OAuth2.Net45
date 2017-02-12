using Murtain.OAuth2.SDK.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.Captcha
{
    /// <summary>
    /// Send message validation code
    /// </summary>
    public class MessageCaptchaSendRequest
    {
        /// <summary>
        /// Message captcha type
        /// </summary>
        [Required]
        public MessageCaptcha MessageCaptchaType { get; set; }
        /// <summary>
        /// Telphone
        /// </summary>
        [Required]
        public string Mobile { get; set; }
        /// <summary>
        /// Expired time（min）
        /// </summary>
        [Required]
        public int ExpiredTime { get; set; }
    }
}
