using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Repositories;
using System.Linq.Expressions;
using IdentityServer3.EntityFramework.Entities;

namespace Murtain.OAuth2.Domain.Repositories
{
    public interface IClientRepository
    {
        IQueryable<Client> GetClients(Expression<Func<Client, bool>> lambda);
        IQueryable<Client> FirstOrDefault(Expression<Func<Client, bool>> lambda);
        Client Add();
        int Count();
        int Count(Expression<Func<Client, bool>> lambda);
        Client Update(Client model);
        Client UpdateProperty(Client model, Expression<Func<Client, object>> lambda);
        Client UpdateCompare(Client model, Client source);
        Client Remove(Client model);

    }
}
