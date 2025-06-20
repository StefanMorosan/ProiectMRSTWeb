using System.Data.Entity;

namespace SpiceMarket_Web.Domain.Models
{
    public class SpiceMarketContext : DbContext
    {
        public SpiceMarketContext() : base("name=SpiceMarketContext") // Connection string name from web.config
        {
        }

        // DbSet for Utilizators table
        public DbSet<Utilizator> Utilizators { get; set; }

        // DbSet for Produse table
        public DbSet<Produs> Produse { get; set; }

        // DbSet for Purchases table
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map Utilizators table
            modelBuilder.Entity<Utilizator>()
                .ToTable("Utilizators");

            // Map Produse table
            modelBuilder.Entity<Produs>()
                .ToTable("Produse");

            // Map Purchases table
            modelBuilder.Entity<Purchase>()
                .ToTable("Purchases");

            base.OnModelCreating(modelBuilder); // Call base method
        }
    }
}