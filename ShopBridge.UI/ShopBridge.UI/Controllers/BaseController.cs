using System;
using System.Web.Mvc;

namespace ShopBridge.UI.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// This method will hit on every request whenever any exception occured in action
        /// It will show the error view
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            var actionName = filterContext.RouteData.Values["action"]?.ToString();
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}