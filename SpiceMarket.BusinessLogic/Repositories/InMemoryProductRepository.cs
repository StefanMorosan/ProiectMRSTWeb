// BusinessLogic/Repositories/InMemoryProductRepository.cs
using System.Linq;

namespace SpiceMarket_Web.BusinessLogic.Repositories
{
    public class InMemoryProductRepository : Interfaces.IProductRepository
    {
        private readonly System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.Produs> _products = new System.Collections.Generic.List<SpiceMarket.Domain.Models.Produs>
        {
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Scorțișoară", Pret = 80m, CaleImagine = "~/Content/images/scortisoara.png", ImageHeight = 120, ImageWidth = 270 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Curcuma", Pret = 70m, CaleImagine = "~/Content/images/curcuma.png", ImageHeight = 195, ImageWidth = 240 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Piper", Pret = 90m, CaleImagine = "~/Content/images/piper.png", ImageHeight = 170, ImageWidth = 210 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Ghimbir", Pret = 60m, CaleImagine = "~/Content/images/ghimbir.png", ImageHeight = 140, ImageWidth = 210 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Paprika", Pret = 75m, CaleImagine = "~/Content/images/paprika.png", ImageHeight = 160, ImageWidth = 210 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Cardamom", Pret = 120m, CaleImagine = "~/Content/images/cardamom.png", ImageHeight = 155, ImageWidth = 175 },
            new SpiceMarket_Web.Domain.Models.Produs { Nume = "Turmeric", Pret = 70m, CaleImagine = "~/Content/images/turmeric.png", ImageHeight = 140, ImageWidth = 210 }
        };

        public SpiceMarket_Web.Domain.Models.Produs GetProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Nume == name);
        }

        public System.Collections.Generic.List<SpiceMarket_Web.Domain.Models.Produs> GetAllProducts()
        {
            return _products;
        }
    }
}