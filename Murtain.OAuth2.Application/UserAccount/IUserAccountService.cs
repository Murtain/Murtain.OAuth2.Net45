using Murtain.Domain.Services;
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
        Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input);
        Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaRequestModel input);
        Task LocalRegistrationAsync(LocalRegistrationRequestModel input);
        Task ValidateGraphicCaptchaAndResendMessageCaptchaAsync(ValidateGraphicCaptchaAndResendMessageCaptchaRequestModel input);
        Task ResetPasswordAsync(ResetPasswordRequestModel resetPasswordRequestModel);
        Task<byte[]> GetLocalRistrationGraphicCaptcha();
        Task<byte[]> GetResetPasswordGraphicCaptcha();
    }
}
