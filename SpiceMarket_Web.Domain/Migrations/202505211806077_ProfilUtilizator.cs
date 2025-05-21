namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilUtilizator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comandas", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comandas", "Email");
        }
    }
}
