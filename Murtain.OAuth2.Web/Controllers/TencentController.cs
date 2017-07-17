using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Murtain.OAuth2.Web.Controllers
{
    public class TencentController : Controller
    {
        private readonly string Token = "B4B83DC748B8E511A2846C0B840997C3";

        public void Server()
        {
            if (string.IsNullOrEmpty(Request.QueryString["echoStr"])) { Response.End(); }

            var echoStr = Request.QueryString["echoStr"].ToString();

            if (!CheckSignature() || string.IsNullOrEmpty(echoStr)) return;

            Response.Write(echoStr);
            Response.End();
        }

        public void SignIn()
        {
            var user = User.Identity;
        }

        private bool CheckSignature()
        {
            var signature = Request.QueryString["signature"].ToString();
            var timestamp = Request.QueryString["timestamp"].ToString();
            var nonce = Request.QueryString["nonce"].ToString();

            var arr = new string[] { Token, timestamp, nonce };
            Array.Sort(arr);

            var tmpStr = string.Join("", arr);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");

            tmpStr = tmpStr?.ToLower();
            return tmpStr == signature;
        }
    }
}