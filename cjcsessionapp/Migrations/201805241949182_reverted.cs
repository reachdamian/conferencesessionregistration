namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reverted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConfirmedEmail", c => c.Boolean(nullable: false));
            DropColumn("dbo.SessionDelegates", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionDelegates", "Title", c => c.String());
            DropColumn("dbo.AspNetUsers", "ConfirmedEmail");
        }
    }
}
