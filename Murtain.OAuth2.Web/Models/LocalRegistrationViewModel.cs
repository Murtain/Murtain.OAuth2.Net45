using Murtain.AutoMapper;
using Murtain.OAuth2.SDK.UserAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Murtain.OAuth2.Web.Models
{

    [AutoMap(typeof(LocalRegistrationRequestModel))]
    public class LocalRegistrationViewModel
    {
        [Required]
        public string Telphone { get; set; }
        [Required]
        public string Captcha { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}