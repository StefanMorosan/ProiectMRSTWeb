namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "ProdusId", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "ProdusId");
            AddForeignKey("dbo.CartItems", "ProdusId", "dbo.Produs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProdusId", "dbo.Produs");
            DropIndex("dbo.CartItems", new[] { "ProdusId" });
            DropColumn("dbo.CartItems", "ProdusId");
        }
    }
}
