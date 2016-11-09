using Murtain.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    public class SetPasswordRequestModel : RequestBase
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Telphone { get; set; }
        ///// <summary>
        ///// 原密码
        ///// </summary>
        //[Required]
        //public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 获取请求服务
        /// </summary>
        /// <returns></returns>
        public override string GetMothod()
        {
            return "UserAccount/SetPassword";
        }
    }


    public class SetPasswordResponseModel : ResponseBase
    {

    }
}
