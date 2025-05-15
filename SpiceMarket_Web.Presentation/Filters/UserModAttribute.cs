using System;
using System.Web.Mvc;

namespace SpiceMarket_Web.Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var username = filterContext.HttpContext.Session["Utilizator"] as string;
            if (string.IsNullOrEmpty(username))
            {
                filterContext.Result = new RedirectResult("~/Home/Autentificare");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
