
namespace SpiceMarket_Web.BusinessLogic.Interfaces
{
    public interface IProductRepository
    {
        SpiceMarket_Web.Domain.Models.Produs GetProductByName(string name);
        System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.Produs> GetAllProducts();
    }
}