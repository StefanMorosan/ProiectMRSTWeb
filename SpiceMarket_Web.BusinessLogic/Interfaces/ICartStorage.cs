// BusinessLogic/Interfaces/ICartStorage.cs
using SpiceMarket_Web.Domain.Models;

namespace SpiceMarket_Web.BusinessLogic.Interfaces
{
    public interface ICartStorage
    {
        System.Collections.Generic.List<CartItem> GetCart();
        void SaveCart(System.Collections.Generic.List<CartItem> cart);
    }
}