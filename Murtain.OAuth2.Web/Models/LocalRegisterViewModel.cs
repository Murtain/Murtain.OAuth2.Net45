﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using IdentityServer3.Core.ViewModels;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.AutoMapper;

namespace Murtain.OAuth2.Web.Models
{
    [AutoMap(typeof(RegisterWithTelphoneRequestModel))]
    public class ValidateImageCaptchaViewModel : ErrorViewModel
    {
        [Required]
        public string Telphone { get; set; }

        [Required]
        public string Captcha { get; set; }
    }
}