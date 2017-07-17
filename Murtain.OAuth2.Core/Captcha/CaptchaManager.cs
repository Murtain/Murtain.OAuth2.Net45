using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Murtain.Caching;
using Murtain.GlobalSettings;
using Murtain.Net.Mail;
using Murtain.OAuth2.SDK.Enum;
using Murtain.Utils;
using Castle.Core.Logging;
using Murtain.Runtime.Cookie;
using Murtain.Runtime.Security;
using Murtain.Exceptions;
using Murtain.GlobalSettings.Extensions;
using Murtain.Web.Exceptions;
using Murtain.Caching.Extensions;
using Murtain.Extensions;

namespace Murtain.OAuth2.Core
{

    public class CaptchaManager : LocalizationService, ICaptchaManager
    {

        private readonly ICacheManager cacheManager;
        private readonly IGlobalSettingManager globalSettingManager;
        private readonly IEmailSender emailSender;

        public ILogger Logger { get; set; }


        public CaptchaManager(IGlobalSettingManager globalSettingManager, ICacheManager cacheManager, IEmailSender emailSender)
        {
            this.globalSettingManager = globalSettingManager;
            this.cacheManager = cacheManager;
            this.emailSender = emailSender;

            Logger = NullLogger.Instance;
        }

        public async Task MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        {
            // generate captcha code
            var code = StringHelper.GenerateCaptcha();

            try
            {
                // call message captcha api

                var client = new HttpClient();

                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"phoneNumber", mobile},
                    {"content", string.Format(globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageContentTemplate),DateTime.Now.ToString(), code)},
                    {"deptType", globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageDeptType) },
                    {"besType", globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageBesType) }
                });

                var response = client.PostAsync(globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageSeverUrl), content);
                var result = await response.Result.Content.ReadAsStringAsync();

                if (!result.Equals("OK", StringComparison.OrdinalIgnoreCase))
                {
                    throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_SEND_FAILED);
                }

                // set cache
                cacheManager.TrySet(Constants.CacheNames.MessageCaptcha, code, type.TryString(), mobile, code);

            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);

                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.SMS_SERVICE_NOT_AVAILABLE);
            }
        }
        public async Task EmailCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string email, int expiredTime)
        {
            // generate captcha code
            string code = StringHelper.GenerateCaptcha();
            try
            {
                // send email
                await emailSender.SendAsync(email, "账号绑定邮箱安全通知", @"
                    <p>亲爱的用户，您好</p>
                    <p>您的验证码是：" + code + @"</p>
                    <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
                    <p>本邮件由系统自动发送，请勿直接回复！</p>
                    <p>感谢您的访问，祝您使用愉快！</p>
                    <p>此致</p>
                    <p>IT应用支持</p>
                ");

                // set cache
                cacheManager.TrySet(Constants.CacheNames.EmailCaptcha, code, type.TryString(), email, code);

            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.EMAIL_CAPTCHA_SEND_FAILED);
            }
        }

        public Task<byte[]> GenderatorGraphicCaptchaAsync(string cookiename)
        {
            // build and return a graphic captcha bytes 
            return Task.FromResult(GraphicCaptchaManager.GetBytes(cookiename));
        }

        public Task ValidateMessageCaptchaAsync(MESSAGE_CAPTCHA_TYPE captchaType, string mobile, string code)
        {
            // try get cache data
            var cache = cacheManager.TryGet<string>(Constants.CacheNames.MessageCaptcha, captchaType.TryString(), mobile, code);

            // if cache data expired 
            if (cache == null)
            {
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_IS_EXPIRED);
            }
            // if cache data not match
            if (cache != code)
            {
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_NOT_MATCHA);
            }

            return Task.FromResult(0);
        }

        public Task ValidateEmailCaptchaAsync(MESSAGE_CAPTCHA_TYPE captchaType, string email, string code)
        {
            // try get cache data
            var cache = cacheManager.TryGet<string>(Constants.CacheNames.EmailCaptcha, captchaType.TryString(), email, code);

            // if cache data expired 
            if (cache == null)
            {
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_IS_EXPIRED);
            }

            // if cache data not match
            if (cache != code)
            {
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_NOT_MATCHA);
            }

            return Task.FromResult(0);
        }


        public Task ValidateGraphicCaptchaAsync(string cookiename, string captcha)
        {
            // if cookie value not match
            if (CookieManager.GetCookieValue(cookiename) != CryptoManager.EncryptDES(captcha))
            {
                throw new UserFriendlyExceprion(CAPTCHA_MANAGER_RETURN_CODE.INVALID_GRAPHIC_CAPTCHA);
            }

            return Task.FromResult(0);
        }

        public Task MessageCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE captcha, string mobile, int expiredTime)
        {
            throw new NotImplementedException();
        }

        public Task EmailCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        {
            throw new NotImplementedException();
        }
    }
}
