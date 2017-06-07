using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;

namespace Murtain.OAuth2.Web.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
                {
                    ////////////////////////
                    // identity scopes
                    ////////////////////////
                  
                    //StandardScopes.OpenId,
                    //StandardScopes.Profile,
                    //StandardScopes.Email,
                    //StandardScopes.Address,
                    //StandardScopes.OfflineAccess,
                    //StandardScopes.RolesAlwaysInclude,
                    //StandardScopes.AllClaims,

                    ////////////////////////
                    // resource scopes
                    ////////////////////////

                    new Scope
                    {
                        Name = "read",
                        DisplayName = "Read data",
                        Type = ScopeType.Resource,
                        Emphasize = false,

                        ScopeSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        }
                    },
                    new Scope
                    {
                        Name = "write",
                        DisplayName = "Write data",
                        Type = ScopeType.Resource,
                        Emphasize = true,

                        ScopeSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        }
                    },
                    new Scope
                    {
                        Name = "idmgr",
                        DisplayName = "IdentityManager",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                        ShowInDiscoveryDocument = false,

                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim(Constants.ClaimTypes.Name),
                            new ScopeClaim(Constants.ClaimTypes.Role)
                        }
                    },
                    new Scope
                    {
                        Name = "user_info",
                        DisplayName = "用户信息",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                        ShowInDiscoveryDocument = true,

                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim(ClaimTypes.name),
                            new ScopeClaim(ClaimTypes.mobile),
                            new ScopeClaim(ClaimTypes.email),
                            new ScopeClaim(ClaimTypes.age),
                            new ScopeClaim(ClaimTypes.sex),
                            new ScopeClaim(ClaimTypes.city),
                            new ScopeClaim(ClaimTypes.address),
                            new ScopeClaim(ClaimTypes.birthday),
                        }
                    },
                    new Scope
                        {
                            Enabled = true,
                            DisplayName = "Protected API",
                            Name = "protected_api_murtain_oauth2_webapi",
                            Description = "Access to a Protected API",
                            Type = ScopeType.Resource
                        }
                };

            scopes.AddRange(StandardScopes.All);

            return scopes;
        }


        public static class ClaimTypes
        {
            public const string name = "name";
            public const string sex = "sex";
            public const string age = "age";
            public const string city = "city";
            public const string mobile = "mobile";
            public const string email = "email";
            public const string birthday = "birthday";
            public const string address = "address";
        }
    }
}