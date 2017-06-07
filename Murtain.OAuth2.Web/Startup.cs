﻿using IdentityServer3.Core.Configuration;
using IdentityServer3.WsFederation.Configuration;
using IdentityServer3.WsFederation.Models;
using IdentityServer3.WsFederation.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Tencent.QQ;
using Microsoft.Owin.Security.Tencent.Wechat;
using Microsoft.Owin.Security.WsFederation;
using Murtain.OAuth2.Web.Configuration;
using Owin;
using System.Collections.Generic;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Owin.Security.Providers.Weixin;
using Serilog;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityServer3.Core.Models;
using System.Web.Helpers;
using System.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(Murtain.OAuth2.Web.Startup))]
namespace Murtain.OAuth2.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //反跨站点请求伪造  
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.File("C:\\Users\\songchaoxu\\Desktop\\log.html")
              .CreateLogger();

            app.UseIdentityServer(new IdentityServerOptions
            {
                IssuerUri = "https://localhost:44373/",                                                                //令牌颁发者Uri
                SiteName = "x-dva passport",                                                        //站点名称
                SigningCertificate = Certificate.Get(),                                             //X.509证书（和相应的私钥签名的安全令牌）
                RequireSsl = false,                                                                  //必须为SSL,默认为True
                Endpoints = new EndpointOptions                                                     //允许启用或禁用特定的端点（默认的所有端点都是启用的）。
                {
                    EnableCspReportEndpoint = false
                },
                Factory = IdentityServer3Factory.Configure("DefaultConnection"),                    //自定义配置
                PluginConfiguration = PluginConfiguration,                                          //插件配置,允许添加协议插件像WS联邦支持。
                ProtocolLogoutUrls = new List<string>                                               //配置回调URL，应该叫中登出（主要协议插件有用）。
                {

                },
                LoggingOptions = new LoggingOptions                                                 //日志配置
                {

                },
                CspOptions = new CspOptions
                {
                    Enabled = false,
                },
                EnableWelcomePage = true,                                                            //启用或禁用默认的欢迎页。默认为True
                AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions //授权配置
                {
                    EnablePostSignOutAutoRedirect = true,

                    IdentityProviders = ConfigureIdentityProviders,
                    LoginPageLinks = new List<LoginPageLink> {
                            new LoginPageLink{ Text = "忘记密码？", Href = "#forgot-password"},
                            new LoginPageLink{ Text = "立即注册", Href = "#local-registration"}
                       }
                },

                EventsOptions = new EventsOptions                                                   //事件配置
                {
                    RaiseSuccessEvents = true,
                    RaiseErrorEvents = true,
                    RaiseFailureEvents = true,
                    RaiseInformationEvents = true
                }
            });
            //app.Map("/", idsrvApp =>
            //{
            //    idsrvApp.UseIdentityServer(new IdentityServerOptions
            //    {
            //        IssuerUri = "https://localhost:44373/",                                                                //令牌颁发者Uri
            //        SiteName = "x-dva passport",                                                        //站点名称
            //        SigningCertificate = Certificate.Get(),                                             //X.509证书（和相应的私钥签名的安全令牌）
            //        RequireSsl = false,                                                                  //必须为SSL,默认为True
            //        Endpoints = new EndpointOptions                                                     //允许启用或禁用特定的端点（默认的所有端点都是启用的）。
            //        {
            //            EnableCspReportEndpoint = false
            //        },
            //        Factory = IdentityServer3Factory.Configure("DefaultConnection"),                    //自定义配置
            //        PluginConfiguration = PluginConfiguration,                                          //插件配置,允许添加协议插件像WS联邦支持。
            //        ProtocolLogoutUrls = new List<string>                                               //配置回调URL，应该叫中登出（主要协议插件有用）。
            //        {

            //        },
            //        LoggingOptions = new LoggingOptions                                                 //日志配置
            //        {

            //        },
            //        CspOptions = new CspOptions
            //        {
            //            Enabled = false,
            //        },
            //        EnableWelcomePage = true,                                                            //启用或禁用默认的欢迎页。默认为True
            //        AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions //授权配置
            //        {
            //            EnablePostSignOutAutoRedirect = true,

            //            IdentityProviders = ConfigureIdentityProviders,
            //            LoginPageLinks = new List<LoginPageLink> {
            //                new LoginPageLink{ Text = "忘记密码？", Href = "#forgot-password"},
            //                new LoginPageLink{ Text = "立即注册", Href = "#local-registration"}
            //           }
            //        },

            //        EventsOptions = new EventsOptions                                                   //事件配置
            //        {
            //            RaiseSuccessEvents = true,
            //            RaiseErrorEvents = true,
            //            RaiseFailureEvents = true,
            //            RaiseInformationEvents = true
            //        }
            //    });
            //});

            //app.Map("/identity", idsrvApp =>
            //{
            //    idsrvApp.UseIdentityServer(new IdentityServerOptions
            //    {
            //        SiteName = "Embedded IdentityServer",
            //        SigningCertificate = Certificate.Get(),

            //        Factory = new IdentityServerServiceFactory()
            //                    .UseInMemoryUsers(Users.Get())
            //                    .UseInMemoryClients(Clients.Get())
            //                    .UseInMemoryScopes(StandardScopes.All)
            //    });
            //});
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Cookies",
            //});
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            //{
            //    Authority = "https://localhost:44373/core",
            //    ClientId = "murtain_x_dva",
            //    RedirectUri = "https://localhost:44330/",
            //    ResponseType = "id_token",

            //    SignInAsAuthenticationType = "Cookies"
            //});
            //app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            //{
            //    MetadataAddress = "https://localhost:44373/" + "/core/wsfed/metadata",
            //    Wtrealm = "urn:owinrp",
            //    SignInAsAuthenticationType = "Cookies"
            //});
        }

        private void PluginConfiguration(IAppBuilder pluginApp, IdentityServerOptions options)
        {
            var wsFedOptions = new WsFederationPluginOptions(options);

            // data sources for in-memory services
            wsFedOptions.Factory.Register(new Registration<IEnumerable<RelyingParty>>(RelyingParties.Get()));
            wsFedOptions.Factory.RelyingPartyService = new Registration<IRelyingPartyService>(typeof(InMemoryRelyingPartyService));

            pluginApp.UseWsFederationPlugin(wsFedOptions);
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseTencentAuthentication(new TencentAuthenticationOptions()
            {
                AuthenticationType = "QQ",
                Caption = "QQ登录",
                SignInAsAuthenticationType = signInAsType,

                AppId = "1105658137",
                AppKey = "MTupHoMGikILy8kw "
            });

            app.UseWeChatAuthentication(new TencentWeChatAuthenticationOptions()
            {
                AuthenticationType = "WeChat",
                Caption = "微信登录",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wxe74f55e0fa310f0b",
                AppSecret = "dd34fff1fe342ad762ccd5c91b561693",
                AuthorizationEndpoint = "https://open.weixin.qq.com/connect/oauth2/authorize", //微信内部网页授权地址,默认PC扫码登录地址
                Scope = { "get_user_info" },
            });
        }

    }
}