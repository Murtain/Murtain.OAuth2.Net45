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
using IdentityServer3.Core.Services.Default;
using Murtain.OAuth2.SDK.Enum;
using Murtain.Web.Exceptions;

namespace Murtain.OAuth2.Application.UserAccount
{
    public class UserAccountService : IUserAccountService
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
            // validate message captcha 
            await captchaManager.ValidateMessageCaptchaAsync(input.CaptchaType, input.Mobile, input.Captcha);
        }


        public async Task LocalRegistrationAsync(LocalRegistrationRequestModel input)
        {
            await userAccountManager.LocalRegistrationAsync(input.Mobile, input.Password);
        }


        public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaRequestModel input)
        {
            // try find user by mobile
            var user = await userAccountManager.FindAsync(input.Mobile);

            switch (input.CaptchaType)
            {
                case MESSAGE_CAPTCHA_TYPE.REGISTER:

                    // validate user mobile is exsit
                    if (user != null)
                    {
                        throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.USER_ALREADY_EXISTS);
                    }

                    // validate register graphic captcha 
                    await captchaManager.ValidateGraphicCaptchaAsync(Constants.CookieNames.LocalRistrationGraphicCaptcha, input.GraphicCaptcha);

                    // send register message captcha and it valid 10 minutes
                    await captchaManager.MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE.REGISTER, input.Mobile, 10);

                    break;

                case MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD:

                    // validate user mobile is exsit

                    if (user == null)
                    {
                        throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.USER_NOT_EXISTS);
                    }
                    // validate retrieve password graphic captcha 
                    await captchaManager.ValidateGraphicCaptchaAsync(Constants.CookieNames.LocalRistrationResetPasswordGraphicCaptcha, input.GraphicCaptcha);

                    // send retrieve password message captcha
                    await captchaManager.MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD, input.Mobile, 10);
                    break;

                default:
                    break;
            }
        }


        public async Task RetrievePasswordAsync(RetrievePasswordRequestModel input)
        {
            // try find user by mobile
            var user = await userAccountManager.FindAsync(input.Mobile);
        }

        public Task<byte[]> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE type)
        {

            switch (type)
            {
                case MESSAGE_CAPTCHA_TYPE.REGISTER:

                    // register graphic captcha
                    return Task.FromResult(GraphicCaptchaManager.GetBytes(Constants.CookieNames.LocalRistrationGraphicCaptcha));

                case MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD:

                    // reset password graphic captcha
                    return Task.FromResult(GraphicCaptchaManager.GetBytes(Constants.CookieNames.LocalRistrationResetPasswordGraphicCaptcha));

                default:
                    break;
            }

            throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.INVALID_GRAPHIC_CAPTCHA);
        }

        public async Task ResendMessageCaptchaAsync(ResendMessageCaptchaRequestModel input)
        {
            await captchaManager.MessageCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD, input.Mobile, 10);
        }
    }
}
