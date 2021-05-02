namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountCourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "ApplicationUser_Id");
            AddForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Courses", "ApplicationUser_Id");
        }
    }
}
