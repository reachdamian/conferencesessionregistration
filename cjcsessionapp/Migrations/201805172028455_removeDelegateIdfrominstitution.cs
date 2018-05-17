namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDelegateIdfrominstitution : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Institutions", "DelegateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institutions", "DelegateId", c => c.Int(nullable: false));
        }
    }
}
