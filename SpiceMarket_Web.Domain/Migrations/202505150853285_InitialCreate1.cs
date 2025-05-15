namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilizators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeUtilizator = c.String(),
                        Parola = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Utilizators");
        }
    }
}
