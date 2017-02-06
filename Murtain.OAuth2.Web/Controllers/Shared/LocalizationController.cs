using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Murtain.Runtime.Session;
using Murtain.Localization;
using Murtain.GlobalSettings;
using Murtain.Localization.Sources;
using System.Globalization;
using System.Text;
using Murtain.Web.Models;
using Murtain.Exceptions;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web.Mvc.Async;

namespace Murtain.OAuth2.Web.Controllers.Shared
{
    public abstract class LocalizationController : Controller
    {
        public IAppSession AppSession { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }

        public LocalizationController()
        {
            LocalizationManager = NullLocalizationManager.Instance;
            AppSession = NullAppSession.Instance;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new NewtonJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }
        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }
        public new JsonResult Json(object data)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public string L(string messageId)
        {
            return LocalizationManager.GetSource(Murtain.OAuth2.Core.Constants.Localization.SourceName.Messages).GetString(messageId);
        }
    }
}