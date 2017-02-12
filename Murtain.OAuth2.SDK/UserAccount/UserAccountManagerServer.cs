using Murtain.SDK.Attributes;
using System.ComponentModel;
using System.Net;

namespace Murtain.OAuth2.Core.UserAccount
{
    public enum UserAccountManagerServer
    {
        [Description("手机号码已存在")]
        [HttpCorresponding(HttpStatusCode.BadRequest)]
        USER_ALREADY_EXISTS = 20000
    }
}