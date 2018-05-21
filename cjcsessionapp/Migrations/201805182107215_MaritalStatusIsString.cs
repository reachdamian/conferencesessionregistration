namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaritalStatusIsString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SessionDelegates", "MartialStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SessionDelegates", "MartialStatus", c => c.Boolean(nullable: false));
        }
    }
}
