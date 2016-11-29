using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.Caching;
using Murtain.OAuth2.Core;

namespace Murtain.OAuth2.Application.UserAccount
{
    public class UserAccountService : ApplicationServiceBase, IUserAccountService
    {
        private readonly ICacheManager cacheManager;
        private readonly ICaptchaManager captchaManager;
        private readonly IUserAccountManager userAccountManager;

        public Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaRequestModel input)
        {
            throw new NotImplementedException();
        }

        public Task LocalRegistrationAsync(LocalRegistrationRequestModel input)
        {
            throw new NotImplementedException();
        }

        public Task ValidateGraphicCaptchaAndResendMessageCaptchaAsync(ValidateGraphicCaptchaAndResendMessageCaptchaRequestModel input)
        {
            throw new NotImplementedException();
        }

        public Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(ResetPasswordRequestModel resetPasswordRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetLocalRistrationGraphicCaptcha()
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetResetPasswordGraphicCaptcha()
        {
            throw new NotImplementedException();
        }
    }
}
