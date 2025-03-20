using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SpiceMarket_Web.DataAccess;
using SpiceMarket_Web.Models;

namespace SpiceMarket_Web.BusinessLogic
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private static List<string> _cosCumparaturi = new List<string>(); // Simulating cart storage

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductViewModel> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Mapper.Map<List<ProductViewModel>>(products); // Map to view model
        }

        public void AddToCart(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                _cosCumparaturi.Add(productName); // Business logic for adding to cart
            }
        }

        public void RemoveFromCart(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                _cosCumparaturi.Remove(productName); // Business logic for removing from cart
            }
        }

        public List<string> GetCartItems()
        {
            return _cosCumparaturi; // Return cart items
        }
    }
}