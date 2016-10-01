using Murtain.GlobalSettings;
using Murtain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core
{
    public abstract class LocalizationService
    {
        public ILocalizationManager LocalizationManager { get; set; }

        public LocalizationService()
        {
            this.LocalizationManager = NullLocalizationManager.Instance;
        }

        public string L(string name)
        {
            return LocalizationManager.GetSource(Constants.Localization.SourceName.Messages).GetString(name);
        }

        public string L(string sourceName, string name)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name);
        }

    }
}
