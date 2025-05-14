// BusinessLogic/Services/CartService.cs
using SpiceMarket_Web.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SpiceMarket_Web.BusinessLogic.Services
{
    public class CartService
    {
        private readonly Interfaces.IProductRepository _productRepository;
        private readonly Interfaces.ICartStorage _cartStorage;

        public CartService(Interfaces.IProductRepository productRepository, Interfaces.ICartStorage cartStorage)
        {
            _productRepository = productRepository;
            _cartStorage = cartStorage;
        }

        public void AddToCart(string productName, decimal quantity)
        {
            var product = _productRepository.GetProductByName(productName);
            if (product == null)
                throw new System.Exception("Produsul nu a fost găsit.");

            var cart = _cartStorage.GetCart();
            var item = cart.FirstOrDefault(i => i.Nume == productName);
            if (item != null)
            {
                item.Cantitate += quantity;
            }
            else
            {
                cart.Add(new CartItem{ Nume = productName, Cantitate = quantity, Pret = product.Pret });
            }
            _cartStorage.SaveCart(cart);
        }

        public void RemoveFromCart(string productName)
        {
            var cart = _cartStorage.GetCart();
            var item = cart.FirstOrDefault(i => i.Nume == productName);
            if (item != null)
            {
                if (item.Cantitate > 1)
                {
                    item.Cantitate--;
                }
                else
                {
                    cart.Remove(item);
                }
                _cartStorage.SaveCart(cart);
            }
        }

        public List<CartItem> GetCartItems()
        {
            return _cartStorage.GetCart();
        }

        public decimal CalculateTotal()
        {
            var cart = _cartStorage.GetCart();
            return cart.Sum(i => i.Cantitate * i.Pret);
        }
    }
}