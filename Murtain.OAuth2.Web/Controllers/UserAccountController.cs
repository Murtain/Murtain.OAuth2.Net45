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
using Murtain.SDK.Attributes;
using Murtain.OAuth2.SDK.Enum;
using Murtain.Extensions;

namespace Murtain.OAuth2.Web.Controllers
{
    /// <summary>
    /// 用户账户服务
    /// </summary>
    [ValidateModel]
    public class UserAccountController : ApiController
    {

        private readonly IUserAccountService userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        /// <summary>
        /// 本地注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/account")]
        public async Task LocalRegistrationAsync([FromBody]LocalRegistrationRequestModel input)
        {
            await userAccountService.LocalRegistrationAsync(input);
        }
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/account/password")]
        public async Task RetrievePasswordAsync([FromBody]RetrievePasswordRequestModel input)
        {
            await userAccountService.RetrievePasswordAsync(input);
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/account/sms")]
        [JsonSample(typeof(ValidateGraphicCaptchaAndSendMessageCaptchaSample))]
        [ReturnCode(typeof(VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE))]
        public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync([FromBody]ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input)
        {
            await userAccountService.ValidateGraphicCaptchaAndSendMessageCaptchaAsync(input);
        }
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [ReturnCode(typeof(VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE))]
        [Route("api/account/sms-validate")]
        [JsonSample(typeof(ValidateMessageCaptchaSample))]
        public async Task ValidateMessageCaptchaAsync([FromBody] ValidateMessageCaptchaRequestModel input)
        {
            await userAccountService.ValidateMessageCaptchaAsync(input);
        }
    }

}
