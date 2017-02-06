using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Services;
using Murtain.OAuth2.SDK.Captcha;

namespace Murtain.OAuth2.Core
{
    public interface ICaptchaManager : IApplicationService
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="captcha">短信验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="expiredTime">失效时间（分钟）</param>
        /// <returns></returns>
        Task MessageCaptchaSendAsync(MessageCaptcha captcha, string mobile, int expiredTime);
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <returns></returns>
        Task ValidateMessageCaptchaAsync(MessageCaptcha captcha, string mobile,string captha);
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task EmailCaptchaSendAsync(MessageCaptcha captcha, string mobile, int expiredTime);
        /// <summary>
        /// 验证邮件验证码
        /// </summary>
        /// <returns></returns>
        Task ValidateEmailCaptchaAsync();
        /// <summary>
        /// 生成图形验证码
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        Task<byte[]> GenderatorGraphicCaptchaAsync(string cookiename);
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        Task ValidateGraphicCaptchaAsync(string cookiename, string captcha);
    }
}
