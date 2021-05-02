namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "IsVisible", c => c.Boolean(nullable: false));
            DropColumn("dbo.Courses", "IsVisible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "IsVisible", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tests", "IsVisible");
        }
    }
}
