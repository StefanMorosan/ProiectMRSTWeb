// BusinessLogic/Interfaces/ICartStorage.cs
namespace SpiceMarket_Web.BusinessLogic.Interfaces
{
    public interface ICartStorage
    {
        System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem> GetCart();
        void SaveCart(System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.CartItem> cart);
    }
}