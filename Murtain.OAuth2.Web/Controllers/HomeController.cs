using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Murtain.OAuth2.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View((User as ClaimsPrincipal)?.Claims);
        }

        [Authorize]
        public ActionResult Authorize()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public async Task<JsonResult> GetAccessTokenAsync()
        {
            var token = await GetTokenAsync();
            return Json(token, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetUserInfoAsync()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token").Value;
            return await GetUserInfoAsync(token);
        }


        private async Task<string> GetUserInfoAsync(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var json = await client.GetStringAsync("https://localhost:44373/core/connect/userinfo");
            return JArray.Parse(json).ToString();
        }
        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new TokenClient(
                "https://localhost:44373/core/connect/token",
                "murtain_oauth2_web",
                "secret");

            return await client.RequestClientCredentialsAsync("user_info");
        }



    }

}