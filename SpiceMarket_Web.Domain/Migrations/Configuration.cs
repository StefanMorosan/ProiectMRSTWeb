namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SpiceMarket_Web.Domain.Models.SpiceMarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; // Enable automatic migrations
            AutomaticMigrationDataLossAllowed = true; // Allow migrations with data loss (use cautiously)
        }

        protected override void Seed(SpiceMarket_Web.Domain.Models.SpiceMarketContext context)
        {
            // Seed data here if necessary
            // Example:
            // context.Utilizators.AddOrUpdate(
            //    u => u.NumeUtilizator,
            //    new Utilizator { NumeUtilizator = "admin", Parola = "admin123", Email = "admin@example.com", Rol = "Administrator" }
            // );
        }
    }
}
