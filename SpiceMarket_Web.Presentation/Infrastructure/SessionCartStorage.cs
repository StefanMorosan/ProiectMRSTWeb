// Infrastructure/SessionCartStorage.cs
namespace SpiceMarket_Web.Infrastructure
{
    public class SessionCartStorage : SpiceMarket_Web.BusinessLogic.Interfaces.ICartStorage
    {
        private readonly System.Web.HttpContextBase _httpContext;

        public SessionCartStorage(System.Web.HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem> GetCart()
        {
            return _httpContext.Session["CosCumparaturi"] as System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem> ?? new System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem>();
        }

        public void SaveCart(System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem> cart)
        {
            _httpContext.Session["CosCumparaturi"] = cart;
        }
    }
}