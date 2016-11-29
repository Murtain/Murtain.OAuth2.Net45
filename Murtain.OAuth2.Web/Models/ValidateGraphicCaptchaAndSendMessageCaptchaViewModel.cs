using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using IdentityServer3.Core.ViewModels;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.AutoMapper;
using Murtain.OAuth2.SDK.Captcha;

namespace Murtain.OAuth2.Web.Models
{
    [AutoMap(typeof(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel))]
    public class ValidateGraphicCaptchaAndSendMessageCaptchaViewModel : ErrorViewModel
    {
        [Required]
        public MessageCaptcha MessageCaptchaType { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string GraphicCaptcha { get; set; }
    }
}