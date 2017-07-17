using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murtain.OAuth2.Web.Controllers
{
    public class ManageController : Controller
    {

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
    }
}