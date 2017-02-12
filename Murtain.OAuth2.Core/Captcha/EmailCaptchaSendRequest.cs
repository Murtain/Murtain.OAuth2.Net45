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
    /// Send email captcha
    /// </summary>
    public class EmailCaptchaSendRequest
    {
        /// <summary>
        /// Email captcha type
        /// </summary>
        [Required]
        public EmailCaptcha EmailCaptchaType { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Expired time（min）
        /// </summary>
        [Required]
        public int ExpiredTime { get; set; }
    }
}
