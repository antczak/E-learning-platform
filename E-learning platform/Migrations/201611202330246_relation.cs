namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.ApplicationUserCourses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Course_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Course_Id);
            
            DropColumn("dbo.Courses", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCourses", new[] { "Course_Id" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserCourses");
            CreateIndex("dbo.Courses", "ApplicationUser_Id");
            AddForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
