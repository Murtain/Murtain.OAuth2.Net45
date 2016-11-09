using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdentityServer3.Core.ViewModels;

namespace Murtain.OAuth2.Web.Models
{
    public class SetPasswordViewModel : ErrorViewModel
    {
        /// <summary>
        /// SignIn
        /// </summary>
        [Required]
        public string SignIn { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Captcha { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }
    }
}