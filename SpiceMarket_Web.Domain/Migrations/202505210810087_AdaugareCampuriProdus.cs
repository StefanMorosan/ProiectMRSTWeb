namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdaugareCampuriProdus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produs", "Ingrediente", c => c.String());
            AddColumn("dbo.Produs", "TaraOrigine", c => c.String());
            AddColumn("dbo.Produs", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produs", "Rating");
            DropColumn("dbo.Produs", "TaraOrigine");
            DropColumn("dbo.Produs", "Ingrediente");
        }
    }
}
