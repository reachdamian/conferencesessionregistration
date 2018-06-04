namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionDelegates", "NameTitle", c => c.String());
            DropColumn("dbo.SessionDelegates", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionDelegates", "Title", c => c.String());
            DropColumn("dbo.SessionDelegates", "NameTitle");
        }
    }
}
