using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Infrastructure
{

    public class ScopeConfigurationDbContext : IdentityServer3.EntityFramework.ScopeConfigurationDbContext
    {
        public ScopeConfigurationDbContext()
            : base("DefaultConnection")
        {

        }
    }
}
