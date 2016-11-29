using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.UserAccount
{
    public class AuthenticateExternalRequest
    {
        public string LoginProvider { get; set; }

        public string LoginProviderId { get; set; }

        public string NickName { get; set; }

        public string Headimageurl { get; set; }
    }
}
