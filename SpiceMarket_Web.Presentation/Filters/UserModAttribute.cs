using System;
using System.Web.Mvc;

namespace SpiceMarket_Web.Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check session for user authentication
            var username = filterContext.HttpContext.Session["Utilizator"] as string;

            if (string.IsNullOrEmpty(username))
            {
                // Fallback to cookies if session is empty
                var usernameCookie = filterContext.HttpContext.Request.Cookies["username"];
                if (usernameCookie != null && !string.IsNullOrEmpty(usernameCookie.Value))
                {
                    // Restore session from cookie
                    filterContext.HttpContext.Session["Utilizator"] = usernameCookie.Value;
                    username = usernameCookie.Value;
                }
            }

            // Redirect to login page if authentication fails
            if (string.IsNullOrEmpty(username))
            {
                filterContext.Result = new RedirectResult("~/Home/Autentificare");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}   