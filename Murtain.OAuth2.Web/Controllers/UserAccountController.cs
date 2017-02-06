using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.OAuth2.Application.UserAccount;
using Murtain.Web.Attributes;

namespace Murtain.OAuth2.Web.Controllers
{
    /// <summary>
    /// 用户账户服务
    /// </summary>
    [ValidateModelAttribute]
    public class UserAccountController : ApiController
    {

        private readonly IUserAccountService userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        [HttpPost]
        [Route("api/account")]
        public async Task LocalRegistrationAsync([FromBody]LocalRegistrationRequestModel input)
        {
            await userAccountService.LocalRegistrationAsync(input);
        }

        [HttpPut]
        [Route("api/account/password")]
        public async Task ResetPasswordAsync([FromBody]ResetPasswordRequestModel input)
        {
            await userAccountService.ResetPasswordAsync(input);
        }

        [HttpPost]
        [Route("api/account/sms")]
        [ResponseCode(typeof(ValidateGraphicCaptchaAndSendMessageCaptcha))]
        public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync([FromBody]ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input)
        {
            await userAccountService.ValidateGraphicCaptchaAndSendMessageCaptchaAsync(input);
        }

        [HttpPut]
        [ResponseCode(typeof(ValidateMessageCaptcha))]
        [Route("api/account/sms-validate")]
        public async Task ValidateMessageCaptchaAsync([FromBody] ValidateMessageCaptchaRequestModel input)
        {
            await userAccountService.ValidateMessageCaptchaAsync(input);
        }
    }
}
