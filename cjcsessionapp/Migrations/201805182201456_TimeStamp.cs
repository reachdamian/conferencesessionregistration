namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionDelegates", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionDelegates", "DateAdded");
        }
    }
}
