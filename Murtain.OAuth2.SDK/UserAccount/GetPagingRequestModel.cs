﻿using Murtain.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace Murtain.OAuth2.SDK.UserAccount
{
    public class GetPagingRequestModel : PagingRequest, IValidate
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public override string GetMothod()
        {
            return "UserAccount/GetPaging";
        }
    }


    public class GetPagingResponseModel : PagingResponse<SDK.UserAccount.UserAccount>
    {

    }
}
