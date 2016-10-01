using Murtain.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Captcha
{
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    public class EmailCaptchaSendRequestModel
    {
        /// <summary>
        /// 邮件验证码类型
        /// </summary>
        [Required]
        public EmailCaptcha Captcha { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// 失效时间（分钟）
        /// </summary>
        [Required]
        public int ExpiredTime { get; set; }
    }
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    public class EmailCaptchaSendResponseModel : ResponseBase
    {
    }

}
