namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tashMay22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConfirmedEmail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmedEmail");
        }
    }
}
