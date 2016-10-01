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

namespace Murtain.OAuth2.Core
{
    public class CaptchaService : LocalizationService, ICaptchaService
    {

        private readonly ICacheManager cacheManager;
        private readonly IGlobalSettingManager globalSettingManager;
        private readonly IEmailSender emailSender;
        public ILogger Logger { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalSettingManager"></param>
        /// <param name="cacheManager"></param>
        /// <param name="emailSender"></param>
        public CaptchaService(IGlobalSettingManager globalSettingManager, ICacheManager cacheManager, IEmailSender emailSender)
        {
            this.globalSettingManager = globalSettingManager;
            this.cacheManager = cacheManager;
            this.emailSender = emailSender;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MessageCaptchaSendResponseModel> MessageCaptchaSendAsync(MessageCaptchaSendRequestModel request)
        {
            try
            {
                var captcha = StringHelper.GenerateRandomNo6();
                var client = new HttpClient();
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"phoneNumber", request.Telphone},
                    {"content", string.Format( await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageContentTemplate),DateTime.Now.ToString(), captcha)},
                    {"deptType", await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageDeptType)},
                    {"besType", await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageBesType)}
                });
                var response = client.PostAsync(await globalSettingManager.GetSettingValueAsync(Constants.Settings.Message.MessageSeverUrl), content);
                var result = await response.Result.Content.ReadAsStringAsync();
                cacheManager.Set(
                    string.Format(Constants.CacheNames.MessageCaptcha, request.Captcha, request.Telphone, captcha),
                    captcha,
                    DateTime.Now.AddMinutes(request.ExpiredTime)
                    );

                return new MessageCaptchaSendResponseModel
                {
                    Ok = result.ToUpper() == "OK",
                    Message = result.ToUpper() == "OK" ? L(Constants.Localization.MessageIds.CAPTCHA_MESSAGE_SEND_SUCCESS) 
                                                       : L(Constants.Localization.MessageIds.CAPTCHA_MESSAGE_SEND_FAIL)
                };
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new MessageCaptchaSendResponseModel
                {
                    Ok = false,
                    Message = L(Constants.Localization.MessageIds.CAPTCHA_MESSAGE_SEND_FAIL_NET)
                };
            }
        }
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmailCaptchaSendResponseModel> EmailCaptchaSendAsync(EmailCaptchaSendRequestModel request)
        {
            var captcha = StringHelper.GenerateRandomNo6();
            try
            {
                cacheManager.Set(
                    string.Format(Constants.CacheNames.EmailCaptcha, request.Captcha, request.Email, captcha),
                    captcha,
                    DateTime.Now.AddMinutes(request.ExpiredTime)
                    );
                await emailSender.SendAsync(request.Email, "账号绑定邮箱安全通知", @"
                    <p>亲爱的用户，您好</p>
                    <p>您的验证码是：" + captcha + @"</p>
                    <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
                    <p>本邮件由系统自动发送，请勿直接回复！</p>
                    <p>感谢您的访问，祝您使用愉快！</p>
                    <p>此致</p>
                    <p>IT应用支持</p>
                ");
                return new EmailCaptchaSendResponseModel
                {
                    Ok = true,
                    Message = L(Constants.Localization.MessageIds.CAPTCHA_EMAIL_SEND_SUCCESS)
                };
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new EmailCaptchaSendResponseModel
                {
                    Ok = false,
                    Message = L(Constants.Localization.MessageIds.CAPTCHA_EMAIL_SEND_FAIL)
                };
            }
        }
    }
}
