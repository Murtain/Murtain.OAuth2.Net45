﻿using Murtain.OAuth2.Core.UserAccount;
using Murtain.SDK;
using Murtain.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    public class LocalRegistrationRequestModel
    {
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public enum LocalRegistration
    {
        [Description("参数无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_PARAMETERS = SystemReturnCode.INVALID_PARAMETERS,

        [Description("手机号码已存在")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        USER_ALREADY_EXISTS = UserAccountManagerServer.USER_ALREADY_EXISTS

    }
}
