using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Infrastructure
{


    public class ClientConfigurationDbContext : IdentityServer3.EntityFramework.ClientConfigurationDbContext
    {
        public ClientConfigurationDbContext()
            : base("DefaultConnection")
        {

        }
    }
}
