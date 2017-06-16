using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

using IdentityServer3.Core.Services;
using IdentityServer3.Core.ViewModels;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Validation;

namespace Murtain.OAuth2.Web.Configuration
{
    public class PassportViewService : IViewService
    {
        private readonly IClientStore clientStore;

        public PassportViewService(IClientStore clientStore)
        {
            this.clientStore = clientStore;
        }

        public async Task<Stream> Login(LoginViewModel model, SignInMessage message)
        {
            var client = await clientStore.FindClientByIdAsync(message.ClientId);
            var name = client != null ? client.ClientName : null;
            return await Render(model, "login", name);
        }

        public async Task<Stream> Logout(LogoutViewModel model, SignOutMessage message)
        {
            return await Render(model, "logout");
        }

        public async Task<Stream> LoggedOut(LoggedOutViewModel model, SignOutMessage message)
        {
            return await Render(model, "loggedOut");
        }

        public async Task<Stream> Consent(ConsentViewModel model, ValidatedAuthorizeRequest authorizeRequest)
        {
            return await Render(model, "consent");
        }

        public async Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
        {
            return await Render(model, "permissions");
        }

        public async Task<Stream> Error(ErrorViewModel model)
        {
            return await Render(model, "error");
        }


        private async Task<Stream> Render(CommonViewModel model, string page, string clientName = null)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });

            string html = LoadHtml(page);
            html = Replace(html, new
            {
                siteName = Microsoft.Security.Application.Encoder.HtmlEncode(model.SiteName),
                model = Microsoft.Security.Application.Encoder.HtmlEncode(json),
                clientName = clientName
            });

            return await Task.FromResult(StringToStream(html));
        }


        private string LoadHtml(string name)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"www\views");
            file = Path.Combine(file, name + ".html");
            return File.ReadAllText(file);
        }

        private string Replace(string value, IDictionary<string, object> values)
        {
            foreach (var key in values.Keys)
            {
                var val = values[key];
                val = val ?? String.Empty;
                if (val != null)
                {
                    value = value.Replace("{" + key + "}", val.ToString());
                }
            }
            return value;
        }

        private string Replace(string value, object values)
        {
            return Replace(value, Map(values));
        }

        private IDictionary<string, object> Map(object values)
        {
            var dictionary = values as IDictionary<string, object>;

            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
                foreach (System.ComponentModel.PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(values));
                }
            }

            return dictionary;
        }

        private Stream StringToStream(string s)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}