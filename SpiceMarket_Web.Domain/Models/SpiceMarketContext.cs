using System.Data.Entity;

namespace SpiceMarket_Web.Domain.Models
{
    public class SpiceMarketContext : DbContext
    {
        public SpiceMarketContext()
            : base("name=SpiceMarketContext")
        {
        }

        public DbSet<Produs> Produse { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Utilizator> Utilizatori { get; set; }
    }
}
