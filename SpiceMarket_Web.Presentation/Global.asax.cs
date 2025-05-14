using System.Web.Mvc;
using System.Web.Routing;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Repositories;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Infrastructure;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity;
using System.Web;

namespace SpiceMarket_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new UnityContainer();

            // Register factory for HttpContextBase
            container.RegisterFactory<HttpContextBase>(c =>
                new HttpContextWrapper(HttpContext.Current));

            // Standard registrations
            container.RegisterType<IProductRepository, InMemoryProductRepository>();

            // Now simply register the cart storage without special constructor logic
            container.RegisterType<ICartStorage, SessionCartStorage>();

            // Register cart service
            container.RegisterType<CartService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}