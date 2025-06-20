namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            // Commented out the creation of the Purchases table as it already exists
            /*
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdusId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            */
        }

        public override void Down()
        {
            // Keep the drop logic for the Purchases table in case you need to roll back
            DropTable("dbo.Purchases");
        }
    }
}