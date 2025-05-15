using System;
using System.Web.Mvc;

namespace SpiceMarket_Web.Presentation.Filters
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var rol = filterContext.HttpContext.Session?["Rol"] as string;

            // Case‑insensitive comparison:
            if (!string.Equals(rol, "admin", StringComparison.OrdinalIgnoreCase))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
