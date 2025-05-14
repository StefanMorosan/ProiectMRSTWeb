using SpiceMarket_Web.Domain.Models;
using System.Collections.Generic;

namespace SpiceMarket_Web.BusinessLogic.Interfaces
{
    public interface IProductRepository
    {
        Produs GetProductByName(string name);
        List<Produs> GetAllProducts();
    }
}