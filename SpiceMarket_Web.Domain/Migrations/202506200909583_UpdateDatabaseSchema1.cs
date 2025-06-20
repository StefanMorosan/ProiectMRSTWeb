namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizators", "RoleLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.Utilizators", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizators", "Email", c => c.String());
            DropColumn("dbo.Utilizators", "RoleLevel");
        }
    }
}
