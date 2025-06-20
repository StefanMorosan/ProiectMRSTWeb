using System.Web;
using System.Web.Mvc;

namespace SpiceMarket_Web.Presentation.Filters
{
    public class AdminModAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Fetch the user's role from the session
            var userRoleLevel = httpContext.Session["RoleLevel"]?.ToString();
            System.Diagnostics.Debug.WriteLine($"AdminModAttribute - Session RoleLevel: {userRoleLevel}");

            // Allow access for Admin or Manager roles
            return userRoleLevel == "admin" || userRoleLevel == "manager";
        }
    }
}