using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Infrastructure
{

    public class OperationalDbContext : IdentityServer3.EntityFramework.OperationalDbContext
    {
        public OperationalDbContext()
            : base("DefaultConnection")
        {

        }
    }
}
