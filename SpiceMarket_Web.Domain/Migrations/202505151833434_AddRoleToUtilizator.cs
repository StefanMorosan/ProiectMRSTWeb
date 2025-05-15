namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleToUtilizator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizators", "Rol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizators", "Rol");
        }
    }
}
