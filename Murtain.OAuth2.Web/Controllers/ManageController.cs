using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murtain.OAuth2.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Security()
        {
            return View();
        }

        public ActionResult Permissions()
        {
            return View();
        }
        public virtual ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return View();
        }
    }
}