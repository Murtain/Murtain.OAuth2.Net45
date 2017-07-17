using Murtain.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.CacheSettingProviders
{
    public class MessageCaptchaCacheSettingProvider : CacheSettingProvider
    {
        public override IEnumerable<CacheSetting> GetCacheSettings()
        {
            return new[] {

             new CacheSetting{
                    Name = Constants.CacheNames.MessageCaptcha,
                    Value ="AUTHORIZATION:MESSAGE_CAPTCHA_CODE:{0}_{1}_{2}",
                    ExpiredTime = TimeSpan.FromMinutes(10),
                    Description = "短信验证码",
                    Group ="短信"
                },

                new CacheSetting{
                    Name = Constants.CacheNames.EmailCaptcha,
                    Value = "AUTHORIZATION:EMAIL_CAPTCH_CODE:{0}_{1}_{2}",
                    ExpiredTime = TimeSpan.FromMinutes(10),
                    Description = "邮件验证码",
                    Group ="短信"
                }
            };
        }
    }
}
