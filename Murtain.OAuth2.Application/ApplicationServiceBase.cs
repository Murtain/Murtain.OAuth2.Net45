using Murtain.Domain.Services;
using Murtain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Application
{
    public abstract class ApplicationServiceBase : IApplicationService
    {
        public ILocalizationManager LocalizationManager { get; set; }

        public ApplicationServiceBase()
        {
            this.LocalizationManager = NullLocalizationManager.Instance;
        }
    }
}
