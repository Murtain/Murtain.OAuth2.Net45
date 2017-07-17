/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using IdentityServer3.EntityFramework.Entities;

namespace Murtain.OAuth2.Infrastructure.Extensions
{
    public static class DbModelBuilderExtensions
    {

        public static void ConfigureClients(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .ToTable("IDX_CLIENT");
            modelBuilder.Entity<Client>()
                .HasMany(x => x.ClientSecrets).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.RedirectUris).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.PostLogoutRedirectUris).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.AllowedScopes).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.IdentityProviderRestrictions).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.Claims).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.AllowedCustomGrantTypes).WithRequired(x => x.Client).WillCascadeOnDelete();
            modelBuilder.Entity<Client>()
                .HasMany(x => x.AllowedCorsOrigins).WithRequired(x => x.Client).WillCascadeOnDelete();

            modelBuilder.Entity<ClientSecret>().ToTable("IDX_CLIENT_SECRET");
            modelBuilder.Entity<ClientRedirectUri>().ToTable("IDX_CLIENT_REDIRECT_URI");
            modelBuilder.Entity<ClientPostLogoutRedirectUri>().ToTable("IDX_CLIENT_POST_LOGOUT_REDIRECT_URI");
            modelBuilder.Entity<ClientScope>().ToTable("IDX_CLIENT_SCOPES");
            modelBuilder.Entity<ClientIdPRestriction>().ToTable("IDX_CLIENT_ID_P_RESTRICTION");
            modelBuilder.Entity<ClientClaim>().ToTable("IDX_CLIENTCLAIM");
            modelBuilder.Entity<ClientCustomGrantType>().ToTable("IDX_CLIENT_CUSTOM_GRANT_TYPE");
            modelBuilder.Entity<ClientCorsOrigin>().ToTable("IDX_CLIENT_CORS_ORIGIN");
        }

        public static void ConfigureConsents(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consent>().ToTable("IDX_CONSENT");
        }

        public static void ConfigureTokens(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>().ToTable("IDX_TOKEN");
        }
        public static void ConfigureScopes(this DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scope>().ToTable("IDX_SCOPE");

            modelBuilder.Entity<Scope>()
                .HasMany(x => x.ScopeClaims).WithRequired(x => x.Scope).WillCascadeOnDelete();
            modelBuilder.Entity<Scope>()
                .HasMany(x => x.ScopeSecrets).WithRequired(x => x.Scope).WillCascadeOnDelete();

            modelBuilder.Entity<ScopeClaim>().ToTable("IDX_SCOPE_CLAIM");
            modelBuilder.Entity<ScopeSecret>().ToTable("IDX_SCOPE_SECRETS");
        }
    }
}