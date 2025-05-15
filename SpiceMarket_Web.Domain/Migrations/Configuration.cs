namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpiceMarket_Web.Domain.Models.SpiceMarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpiceMarket_Web.Domain.Models.SpiceMarketContext context)
        {
        }
    }
}
