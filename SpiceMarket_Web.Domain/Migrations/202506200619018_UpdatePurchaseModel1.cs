namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePurchaseModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Username", c => c.String());
            AddColumn("dbo.Purchases", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Purchases", "Cantitate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "Cantitate", c => c.Int(nullable: false));
            DropColumn("dbo.Purchases", "TotalPrice");
            DropColumn("dbo.Purchases", "Username");
        }
    }
}
