﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.ComponentModel.DataAnnotations;
using Murtain.SDK;
using Murtain.SDK.Attributes;
using Murtain.OAuth2.SDK.Enum;

namespace Murtain.OAuth2.SDK.UserAccount
{

    /// <summary>
    /// 验证短信验证码，若验证类型注册，验证成功后创建账户，若
    /// </summary>
    public class ValidateMessageCaptchaRequestModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Captcha { get; set; }
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
    /// 验证短信验证码返回码
    /// </summary>
    public enum VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE
    {
        /// <summary>
        /// 参数无效
        /// </summary>
        [Description("参数无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_PARAMETERS = SystemReturnCode.INVALID_PARAMETERS,
        /// <summary>
        /// 验证码无效
        /// </summary>
        [Description("验证码无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_CAPTCHA = 21000,
        /// <summary>
        /// 验证码已过期，请重新获取验证码
        /// </summary>
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        [Description("验证码已过期，请重新获取验证码")]
        EXPIRED_CAPTCHA,

    }
}
