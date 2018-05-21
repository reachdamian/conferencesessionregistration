namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserWhoRegisterDelegate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registereds", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Registereds", "ApplicationUserId");
            AddForeignKey("dbo.Registereds", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registereds", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Registereds", new[] { "ApplicationUserId" });
            DropColumn("dbo.Registereds", "ApplicationUserId");
        }
    }
}
