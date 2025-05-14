using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Repositories;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Infrastructure;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity;

namespace SpiceMarket_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new UnityContainer();
            container.RegisterType<IProductRepository, InMemoryProductRepository>();
            container.RegisterType<ICartStorage, SessionCartStorage>(new InjectionConstructor(new ResolvedParameter<System.Web.HttpContextBase>()));
            container.RegisterType<CartService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}