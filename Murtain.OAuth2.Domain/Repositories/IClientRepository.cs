using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Repositories;
using System.Linq.Expressions;

namespace Murtain.OAuth2.Domain.Repositories
{
    public interface IClientRepository
    {
        IQueryable<IdentityServer3.EntityFramework.Entities.Client> GetClients(Expression<Func<IdentityServer3.EntityFramework.Entities.Client, bool>> lambda);

        IQueryable<IdentityServer3.EntityFramework.Entities.Client> FirstOrDefault(Expression<Func<IdentityServer3.EntityFramework.Entities.Client, bool>> lambda);

        IdentityServer3.EntityFramework.Entities.Client Add();

        IdentityServer3.EntityFramework.Entities.Client Update();

        void Remove();



    }
}
