using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Murtain.OAuth2.Web.Configuration
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                /////////////////////////////////////////////////////////////
                // MVC 隐式授权认证客户端（Murtain.OAuth2.MVC.Implicit）
                //
                // 隐式授权
                // 
                //（A）客户端将用户导向认证服务器。
                //
                //      GET /authorize?response_type=token&client_id={client_id}&state={state}&redirect_uri={redirect_uri}
                //
                //      response_type：表示授权类型，此处的值固定为"token"，必选项。
                //      client_id：表示客户端的ID，必选项。
                //      redirect_uri：表示重定向的URI，可选项。
                //      scope：表示权限范围，可选项。
                //      state：表示客户端的当前状态，可以指定任意值，认证服务器会原封不动地返回这个值。
                //
                //（B）用户决定是否给于客户端授权。
                //（C）假设用户给予授权，认证服务器将用户导向客户端指定的"重定向URI"，并在URI的Hash部分包含了访问令牌。
                //
                //      HTTP/1.1 302 Found
                //      Location: http://example.com/cb#access_token={access_token}&state={state}&token_type={token_type}&expires_in=3600
                //
                //      access_token：表示访问令牌，必选项。
                //      token_type：表示令牌类型，该值大小写不敏感，必选项。
                //      expires_in：表示过期时间，单位为秒。如果省略该参数，必须其他方式设置过期时间。
                //      scope：表示权限范围，如果与客户端申请的范围一致，此项可省略。
                //      state：如果客户端的请求中包含这个参数，认证服务器的回应也必须一模一样包含这个参数。
                //（D）浏览器向资源服务器发出请求，其中不包括上一步收到的Hash值。
                //（E）资源服务器返回一个网页，其中包含的代码可以获取Hash值中的令牌。
                //（F）浏览器执行上一步获得的脚本，提取出令牌。
                //（G）浏览器将令牌发给客户端。
                /////////////////////////////////////////////////////////////
                new Client
                {
                    Enabled = true,                                                     
                    ClientName = "MVC 隐式授权认证客户端",
                    ClientId = "mvc_owin_implicit",
                    Flow = Flows.Implicit,
                    AllowAccessToAllScopes = true,                                      //（完全信任，可以访问所有scope资源）
                    

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44366/"
                    },

                    LogoutUri = "https://localhost:44301/Home/SignoutCleanup",
                    LogoutSessionRequired = true,
                },

                /////////////////////////////////////////////////////////////
                // MVC 密码模式授权认证客户端（Murtain.OAuth2.MVC.ResourceOwner）
                //
                // 密码模式
                // 
                //（A）用户向客户端提供用户名和密码。
                //（B）客户端将用户名和密码发给认证服务器，向后者请求令牌。
                //
                //      POST /token HTTP/1.1
                //      Host: server.example.com
                //      Authorization: Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW
                //      Content-Type: application/x-www-form-urlencoded
                //
                //      grant_type=password&username={username }&password={ password}
                //
                //      grant_type：表示授权类型，此处的值固定为"password"，必选项。
                //      username：表示用户名，必选项。
                //      password：表示用户的密码，必选项。
                //      scope：表示权限范围，可选项。
                //
                //（C）认证服务器确认无误后，向客户端提供访问令牌。
                //
                //      HTTP/1.1 200 OK
                //      Content-Type: application/json; charset = UTF - 8
                //      Cache - Control: no - store
                //      Pragma: no - cache
                //
                //      {
                //        "access_token":"2YotnFZFEjr1zCsicMWpAA",
                //        "token_type":"example",
                //        "expires_in":3600,
                //        "refresh_token":"tGzv3JOkF0XG5Qx2TlKWIA",
                //        "example_parameter":"example_value"
                //      }
                /////////////////////////////////////////////////////////////
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC 密码模式授权认证客户端",
                    ClientId = "mvc.owin.password",
                    Flow = Flows.ResourceOwner,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "write",
                        "address",
                        "offline_access"
                    },

                    // used by JS resource owner sample
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:13048"
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,

                    // refresh token settings
                    AbsoluteRefreshTokenLifetime = 86400,
                    SlidingRefreshTokenLifetime = 43200,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding
                },
                /////////////////////////////////////////////////////////////
                // MVC 客户端模式授权认证客户端（Murtain.OAuth2.MVC.ClientCredentials）
                //
                // 客户端模式
                // 
                //（A）客户端向认证服务器进行身份认证，并要求一个访问令牌。
                //     POST /token HTTP/1.1
                //     Host: server.example.com
                //     Authorization: Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW
                //     Content-Type: application/x-www-form-urlencoded
                //
                //     grant_type=client_credentials
                //
                //     granttype：表示授权类型，此处的值固定为"clientcredentials"，必选项。
                //     scope：表示权限范围，可选项。
                //（B）认证服务器确认无误后，向客户端提供访问令牌。
                //
                //      HTTP/1.1 200 OK
                //      Content-Type: application/json; charset = UTF - 8
                //      Cache - Control: no - store
                //      Pragma: no - cache
                //
                //      {
                //        "access_token":"2YotnFZFEjr1zCsicMWpAA",
                //        "token_type":"example",
                //        "expires_in":3600,
                //        "refresh_token":"tGzv3JOkF0XG5Qx2TlKWIA",
                //        "example_parameter":"example_value"
                //      }
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "MVC 客户端模式授权认证客户端",
                    Enabled = true,
                    ClientId = "mvc_owin_clientcredentials",
                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()),
                        new Secret
                        {
                            Value = "61B754C541BBCFC6A45A9E9EC5E47D8702B78C29",
                            Type = Constants.SecretTypes.X509CertificateThumbprint,
                            Description = "Client Certificate"
                        },
                    },

                    AllowAccessToAllScopes =true,
                },

                /////////////////////////////////////////////////////////////
                // MVC 客户授权模式客户端（Murtain.OAuth2.MVC.CustomGrant）
                //
                // 客户授权模式
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "MVC 客户授权模式授权认证客户端",
                    ClientId = "mvc_owin_customgrant",
                    Flow = Flows.Custom,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "read",
                        "write"
                    },

                    AllowedCustomGrantTypes = new List<string>
                    {
                        "custom"
                    }
                },
                /////////////////////////////////////////////////////////////
                // MVC CodeFlowClient Manual
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "Code Flow Client Demo",
                    ClientId = "codeclient",
                    Flow = Flows.AuthorizationCode,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    ClientUri = "https://identityserver.io",

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44312/callback",
                    },

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        "read",
                        "write"
                    },

                    AccessTokenType = AccessTokenType.Reference,
                },

                /////////////////////////////////////////////////////////////
                // MVC No Library Client
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "OpenID Connect without Client Library Sample",
                    ClientId = "nolib.client",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.Address,
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:11716/account/signInCallback",
                    },
                },

                /////////////////////////////////////////////////////////////
                // MVC OWIN Hybrid Client
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "MVC OWIN Hybrid Client",
                    ClientId = "mvc.owin.hybrid",
                    Flow = Flows.Hybrid,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Reference,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44300/"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44300/"
                    },

                    LogoutUri = "https://localhost:44300/Home/OidcSignOut",
                    LogoutSessionRequired = true
                },
                /////////////////////////////////////////////////////////////
                // JavaScript Implicit Client - OAuth only
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "JavaScript Implicit Client - Simple",
                    ClientId = "js.simple",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:37045/index.html",
                    },
                },

                /////////////////////////////////////////////////////////////
                // JavaScript Implicit Client - Manual
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "JavaScript Implicit Client - Manual",
                    ClientId = "js.manual",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:37046/index.html",
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:37046"
                    }
                },

                /////////////////////////////////////////////////////////////
                // JavaScript Implicit Client - TokenManager
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "JavaScript Implicit Client - TokenManager",
                    ClientId = "js.tokenmanager",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://square.x-dva.com/index.html",
                        "http://square.x-dva.com/silent_renew.html",
                        "http://square.x-dva.com/callback.html",
                        "http://square.x-dva.com/frame.html",
                        "http://square.x-dva.com/popup.html",
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://square.x-dva.com/index.html",
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://square.x-dva.com",
                    },

                    AccessTokenLifetime = 3600,
                    AccessTokenType = AccessTokenType.Jwt
                },


                /////////////////////////////////////////////////////////////
                // WebForms OWIN Implicit Client
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "WebForms OWIN Implicit Client",
                    ClientId = "webforms.owin.implicit",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.Address,
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:5969/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5969/"
                    }
                },

                /////////////////////////////////////////////////////////////
                // WPF WebView Client Sample
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "WPF WebView Client Sample",
                    ClientId = "wpf.webview.client",
                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.Address,
                        "read",
                        "write"
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "oob://localhost/wpf.webview.client",
                    },
                },

                /////////////////////////////////////////////////////////////
                // WPF Client with Hybrid Flow and PKCE
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "WPF Client with Hybrid Flow and PKCE",
                    ClientId = "wpf.hybrid",
                    Flow = Flows.HybridWithProofKey,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = new List<string>
                    {
                        "http://localhost/wpf.hybrid"
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        "read", "write"
                    },

                    AccessTokenType = AccessTokenType.Reference
                },
                

                /////////////////////////////////////////////////////////////
                // UWP OIDC Client
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "UWP OIDC Client",
                    ClientId = "uwp",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    Flow = Flows.HybridWithProofKey,

                    RedirectUris = new List<string>
                    {
                        "ms-app://s-1-15-2-491127476-3924255528-3585180829-1321445252-2746266865-3272304314-3346717936/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "ms-app://s-1-15-2-491127476-3924255528-3585180829-1321445252-2746266865-3272304314-3346717936/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid", "profile", "write"
                    },

                    AccessTokenType = AccessTokenType.Reference
                },
            };
        }
    }
}