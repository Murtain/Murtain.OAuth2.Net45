using Murtain.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enum
{
    public enum USER_ACCOUNT_MANAGER_RETURN_CODE
    {
        /// <summary>
        /// 手机号码已存在
        /// </summary>
        [Description("手机号码已存在")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        USER_ALREADY_EXISTS = RETURN_CODE_SEED.USER_ACCOUNT_MANAGER,

        /// <summary>
        /// 手机号未注册
        /// </summary>
        [Description("手机号未注册")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        USER_NOT_EXISTS,

        /// <summary>
        /// 图形验证码类型无效
        /// </summary>
        [Description("图形验证码类型无效")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        INVALID_GRAPHIC_CAPTCHA
    }
}
