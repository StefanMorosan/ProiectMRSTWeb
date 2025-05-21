namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dashboard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comandas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Utilizator = c.String(),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comandas");
        }
    }
}
