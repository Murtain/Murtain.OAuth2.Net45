﻿using System;
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


            factory.LocalizationService = new Registration<ILocalizationService>(resolver => IocManager.Instance.Resolve<ILocalizationService>());

            // These registrations are also needed since these are dealt with using non-standard construction
            factory.Register(new Registration<HttpContext>(resolver => HttpContext.Current));
            factory.Register(new Registration<HttpContextBase>(resolver => new HttpContextWrapper(resolver.Resolve<HttpContext>())));
            factory.Register(new Registration<HttpRequestBase>(resolver => resolver.Resolve<HttpContextBase>().Request));
            factory.Register(new Registration<HttpResponseBase>(resolver => resolver.Resolve<HttpContextBase>().Response));
            factory.Register(new Registration<HttpServerUtilityBase>(resolver => resolver.Resolve<HttpContextBase>().Server));
            factory.Register(new Registration<HttpSessionStateBase>(resolver => resolver.Resolve<HttpContextBase>().Session));


            return factory;
        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Clients.Any())
                {
                    foreach (var c in clients)
                    {
                        var e = c.ToEntity();
                        db.Clients.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }
        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Scopes.Any())
                {
                    foreach (var s in scopes)
                    {
                        var e = s.ToEntity();
                        db.Scopes.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}