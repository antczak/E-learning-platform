namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseCategories", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.TestResults", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "Time");
            DropColumn("dbo.Courses", "Order");
            DropColumn("dbo.CourseCategories", "Order");
        }
    }
}
