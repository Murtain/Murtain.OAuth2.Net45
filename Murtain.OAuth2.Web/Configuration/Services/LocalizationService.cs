using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core;
using IdentityServer3.Core.Services;
using Murtain.Dependency;
using Murtain.Localization;
using Murtain.Domain.Services;
using Murtain.Runtime.Session;
using Constants = Murtain.OAuth2.Core.Constants;

namespace Murtain.OAuth2.Web.Configuration.Services
{

    public class LocalizationService : ILocalizationService,IDependency
    {
        public ILocalizationManager LocalizationManager { get; set; }

        public LocalizationService()
        {
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public virtual string GetString(string category, string id)
        {
           return LocalizationManager.GetSource(category).GetString(id);
        }
    }
}