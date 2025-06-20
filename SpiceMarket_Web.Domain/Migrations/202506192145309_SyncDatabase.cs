namespace SpiceMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizators", "NumeUtilizator", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Utilizators", "Parola", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Utilizators", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizators", "Email", c => c.String());
            AlterColumn("dbo.Utilizators", "Parola", c => c.String());
            AlterColumn("dbo.Utilizators", "NumeUtilizator", c => c.String());
        }
    }
}
