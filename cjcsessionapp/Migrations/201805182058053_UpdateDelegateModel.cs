namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDelegateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionDelegates", "AgeGroup", c => c.String());
            CreateIndex("dbo.Registereds", "SessionDelegateId");
            AddForeignKey("dbo.Registereds", "SessionDelegateId", "dbo.SessionDelegates", "Id", cascadeDelete: true);
            DropColumn("dbo.SessionDelegates", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionDelegates", "Age", c => c.String());
            DropForeignKey("dbo.Registereds", "SessionDelegateId", "dbo.SessionDelegates");
            DropIndex("dbo.Registereds", new[] { "SessionDelegateId" });
            DropColumn("dbo.SessionDelegates", "AgeGroup");
        }
    }
}
