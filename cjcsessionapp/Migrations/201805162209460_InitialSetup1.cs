namespace cjcsessionapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionDelegates", "Pastor", c => c.String());
            AddColumn("dbo.SessionDelegates", "Address", c => c.String());
            AddColumn("dbo.SessionDelegates", "MartialStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Reguar", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Guest", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "DelegateAtLarge", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "SpecialDelegate", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Allergies", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Asthma", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Diabetes", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "HighBloodPressure", c => c.Boolean(nullable: false));
            AddColumn("dbo.SessionDelegates", "BronchialDisorder", c => c.Boolean(nullable: false));
            DropColumn("dbo.SessionDelegates", "Gender");
            DropColumn("dbo.SessionDelegates", "DelegateType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionDelegates", "DelegateType", c => c.String(nullable: false));
            AddColumn("dbo.SessionDelegates", "Gender", c => c.String());
            DropColumn("dbo.SessionDelegates", "BronchialDisorder");
            DropColumn("dbo.SessionDelegates", "HighBloodPressure");
            DropColumn("dbo.SessionDelegates", "Vegetarian");
            DropColumn("dbo.SessionDelegates", "Diabetes");
            DropColumn("dbo.SessionDelegates", "Asthma");
            DropColumn("dbo.SessionDelegates", "Allergies");
            DropColumn("dbo.SessionDelegates", "SpecialDelegate");
            DropColumn("dbo.SessionDelegates", "DelegateAtLarge");
            DropColumn("dbo.SessionDelegates", "Guest");
            DropColumn("dbo.SessionDelegates", "Reguar");
            DropColumn("dbo.SessionDelegates", "MartialStatus");
            DropColumn("dbo.SessionDelegates", "Address");
            DropColumn("dbo.SessionDelegates", "Pastor");
        }
    }
}
