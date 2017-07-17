using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Enum;
using Murtain.OAuth2.SDK.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Application.UserAccount
{
    public interface IUserAccountService : IApplicationService
    {
        /// <summary>
        /// validate graphic captcha and send message captcha
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input);
        /// <summary>
        /// validate message captcha
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaRequestModel input);
        /// <summary>
        /// local registration
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task LocalRegistrationAsync(LocalRegistrationRequestModel input);
        /// <summary>
        /// resend message captcha
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResendMessageCaptchaAsync(ResendMessageCaptchaRequestModel input);
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RetrievePasswordAsync(RetrievePasswordRequestModel input);
        /// <summary>
        /// get graphic captcha bytes
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<byte[]> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE type);
    }
}
