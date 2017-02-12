using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Murtain.Caching;
using Murtain.GlobalSettings;
using Murtain.Net.Mail;
using Murtain.OAuth2.SDK.Captcha;
using Murtain.Utils;
using Castle.Core.Logging;
using Murtain.Runtime.Cookie;
using Murtain.Runtime.Security;
using Murtain.Exceptions;
using Murtain.GlobalSettings.Extensions;
using Murtain.Web.Exceptions;

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

        public async Task MessageCaptchaSendAsync(MessageCaptcha captcha, string mobile, int expiredTime)
        {
            var template = globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageContentTemplate);
            var deptType = globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageDeptType);
            var besType = globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageBesType);
            var url = globalSettingManager.GetSettingValue(Constants.Settings.Message.MessageSeverUrl);

            var code = StringHelper.GenerateCaptcha();
            try
            {
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"phoneNumber", mobile},
                    {"content", string.Format(  template,DateTime.Now.ToString(), code)},
                    {"deptType", deptType },
                    {"besType", besType }
                });

                var response = client.PostAsync(url, content);
                var result = await response.Result.Content.ReadAsStringAsync();

                cacheManager.Set(string.Format(Constants.CacheNames.MessageCaptcha, captcha, mobile, code), code, DateTime.Now.AddMinutes(expiredTime));

                if (!result.ToUpper().Equals("OK"))
                {
                    throw new UserFriendlyExceprion(MessageCaptchaServer.MESSAGE_CAPTCHA_SEND_FAILED);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);

                throw new UserFriendlyExceprion(MessageCaptchaServer.SMS_SERVICE_NOT_AVAILABLE);
            }
        }
        public async Task EmailCaptchaSendAsync(MessageCaptcha captcha, string email, int expiredTime)
        {
            string code = new Random().Next(100000, 999999).ToString();
            try
            {
                cacheManager.Set(string.Format(Constants.CacheNames.EmailCaptcha, captcha, email, code), captcha, DateTime.Now.AddMinutes(expiredTime));

                await emailSender.SendAsync(email, "账号绑定邮箱安全通知", @"
                    <p>亲爱的用户，您好</p>
                    <p>您的验证码是：" + captcha + @"</p>
                    <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
                    <p>本邮件由系统自动发送，请勿直接回复！</p>
                    <p>感谢您的访问，祝您使用愉快！</p>
                    <p>此致</p>
                    <p>IT应用支持</p>
                ");

            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new UserFriendlyExceprion(MessageCaptchaServer.EMAIL_CAPTCHA_SEND_FAILED);
            }
        }
        public Task<byte[]> GenderatorGraphicCaptchaAsync(string cookiename)
        {
            return Task.FromResult(GraphicCaptchaManager.GetBytes(cookiename));
        }
        public Task ValidateMessageCaptchaAsync(MessageCaptcha captchaType, string mobile, string code)
        {
            var cache = cacheManager.Get<string>(string.Format(Constants.CacheNames.MessageCaptcha, captchaType, mobile, code));

            if (cache == null)
            {
                throw new UserFriendlyExceprion(MessageCaptchaServer.MESSAGE_CAPTCHA_IS_EXPIRED);
            }

            if (cache != code)
            {
                throw new UserFriendlyExceprion(MessageCaptchaServer.MESSAGE_CAPTCHA_NOT_MATCHA);
            }

            return Task.FromResult(0);
        }
        public Task ValidateEmailCaptchaAsync()
        {
            throw new NotImplementedException();
        }
        public Task ValidateGraphicCaptchaAsync(string cookiename, string captcha)
        {
            if (CookieManager.GetCookieValue(cookiename) != CryptoManager.EncryptDES(captcha))
            {
                throw new UserFriendlyExceprion(MessageCaptchaServer.INVALID_GRAPHIC_CAPTCHA);
            }

            return Task.FromResult(0);
        }
    }
}
