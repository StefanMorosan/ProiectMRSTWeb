using System.Web;
using System.Web.Mvc;
using SpiceMarket_Web.Domain.Models;

public class AdminModAttribute : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        // Fetch the user's role from the session
        var userRole = httpContext.Session["Rol"]?.ToString();

        // Allow access for Admin or Manager roles
        return userRole == RoleConstants.Admin || userRole == RoleConstants.Manager;
    }
}