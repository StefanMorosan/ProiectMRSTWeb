namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        Cantitate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pret = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nume = c.String(),
                        Pret = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CaleImagine = c.String(),
                        ImageHeight = c.Int(nullable: false),
                        ImageWidth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produs");
            DropTable("dbo.CartItems");
        }
    }
}
