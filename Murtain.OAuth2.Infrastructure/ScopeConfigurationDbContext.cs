using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.OAuth2.Infrastructure.Extensions;

namespace Murtain.OAuth2.Infrastructure
{

    public class ScopeConfigurationDbContext : IdentityServer3.EntityFramework.ScopeConfigurationDbContext
    {
        public ScopeConfigurationDbContext()
            : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ConfigureScopes();
        }
    }
}
