using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IdentityServer3.Core.Configuration;
using IdentityServer3.EntityFramework;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;

using Murtain.Dependency;
using Murtain.OAuth2.Core;
using Murtain.OAuth2.Web.Configuration.Services;
using Murtain.OAuth2.Web.Controllers;
using Murtain.OAuth2.Core.UserAccount;
using Murtain.OAuth2.Application.UserAccount;
using Murtain.OAuth2.Domain.Repositories;
using Murtain.OAuth2.Infrastructure.Repositories;
using Murtain.EntityFramework;
using Murtain.OAuth2.Infrastructure;
using Murtain.Domain.UnitOfWork;

namespace Murtain.OAuth2.Web.Configuration
{
    public class IdentityServer3Factory
    {
        public static IdentityServerServiceFactory Configure(string connString)
        {
            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = connString,
            };

            // these two calls just pre-populate the test DB from the in-memory config
            ConfigureClients(Clients.Get(), efConfig);
            ConfigureScopes(Scopes.Get(), efConfig);

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);


            // These registration to use customer user service and view service
            factory.UserService = new Registration<IUserService, UserService>();
            factory.ViewService = new Registration<IViewService, AccountViewService<AccountController>>();

            factory.LocalizationService = new Registration<ILocalizationService>(resolver => IocManager.Container.Resolve<ILocalizationService>());

            // These registrations are also needed since these are dealt with using non-standard construction
            factory.Register(new Registration<HttpContext>(resolver => HttpContext.Current));
            factory.Register(new Registration<HttpContextBase>(resolver => new HttpContextWrapper(resolver.Resolve<HttpContext>())));
            factory.Register(new Registration<HttpRequestBase>(resolver => resolver.Resolve<HttpContextBase>().Request));
            factory.Register(new Registration<HttpResponseBase>(resolver => resolver.Resolve<HttpContextBase>().Response));
            factory.Register(new Registration<HttpServerUtilityBase>(resolver => resolver.Resolve<HttpContextBase>().Server));
            factory.Register(new Registration<HttpSessionStateBase>(resolver => resolver.Resolve<HttpContextBase>().Session));


            factory.Register(new Registration<IUserAccountManager>(resolver => IocManager.Container.Resolve<IUserAccountManager>()));


            return factory;
        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var db = new IdentityServer3.EntityFramework.ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                foreach (var c in clients)
                {
                    if (db.Clients.Any(x => x.ClientId == c.ClientId))
                    {
                        db.Clients.Remove(db.Clients.First(x => x.ClientId == c.ClientId));
                    }

                    var e = c.ToEntity();
                    db.Clients.Add(e);
                }
                db.SaveChanges();
            }
        }
        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var db = new IdentityServer3.EntityFramework.ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                foreach (var s in scopes)
                {
                    if (db.Scopes.Any(x => x.Name == s.Name))
                    {
                        db.Scopes.Remove(db.Scopes.First(x => x.Name == s.Name));
                    }
                    var e = s.ToEntity();
                    db.Scopes.Add(e);
                }
                db.SaveChanges();
            }
        }
    }
}