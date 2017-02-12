using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.Caching;
using Murtain.OAuth2.Core;
using Murtain.Runtime.Cookie;
using Murtain.Exceptions;
using Murtain.Threading;

namespace Murtain.OAuth2.Application.UserAccount
{
    public class UserAccountService : ApplicationServiceBase, IUserAccountService
    {
        private readonly ICacheManager cacheManager;
        private readonly ICaptchaManager captchaManager;
        private readonly IUserAccountManager userAccountManager;

        public UserAccountService(ICacheManager cacheManager, ICaptchaManager captchaManager, IUserAccountManager userAccountManager)
        {
            this.cacheManager = cacheManager;
            this.captchaManager = captchaManager;
            this.userAccountManager = userAccountManager;
        }

        public async Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaRequestModel input)
        {
            await captchaManager.ValidateMessageCaptchaAsync(SDK.Captcha.MessageCaptcha.Register, input.Mobile, input.Captcha);
        }

        public async Task LocalRegistrationAsync(LocalRegistrationRequestModel input)
        {
           await userAccountManager.LocalRegistrationAsync(input);
        }

        public async Task ValidateGraphicCaptchaAndResendMessageCaptchaAsync(ValidateMessageCaptchaRequestModel input)
        {
            await captchaManager.ValidateMessageCaptchaAsync(SDK.Captcha.MessageCaptcha.Register,input.Mobile,input.Captcha);
        }

        public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input)
        {
            await captchaManager.ValidateGraphicCaptchaAsync(Constants.CookieNames.LocalRistration, input.GraphicCaptcha);
            
            await captchaManager.MessageCaptchaSendAsync(SDK.Captcha.MessageCaptcha.Register, input.Mobile, 10);

        }

        public Task ResetPasswordAsync(ResetPasswordRequestModel resetPasswordRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetLocalRistrationGraphicCaptcha()
        {
            return Task.FromResult(GraphicCaptchaManager.GetBytes(Constants.CookieNames.LocalRistration));
        }

        public Task<byte[]> GetResetPasswordGraphicCaptcha()
        {
            throw new NotImplementedException();
        }

        public Task ResendMessageCaptchaAsync(ResendMessageCaptchaRequestModel input)
        {
            throw new NotImplementedException();
        }
    }
}
