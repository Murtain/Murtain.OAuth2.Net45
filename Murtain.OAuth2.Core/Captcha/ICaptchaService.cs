using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.OAuth2.SDK.Captcha;
using Murtain.Domain.Services;

namespace Murtain.OAuth2.Core
{
    public interface ICaptchaService : IApplicationService
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MessageCaptchaSendResponseModel> MessageCaptchaSendAsync(MessageCaptchaSendRequestModel request);
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmailCaptchaSendResponseModel> EmailCaptchaSendAsync(EmailCaptchaSendRequestModel request);
        /// <summary>
        /// 生成图形验证码
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        Task<byte[]> GenderatorImageCaptcha(string cookiename);
    }
}
