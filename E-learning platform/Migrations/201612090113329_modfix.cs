namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", "Course_Id1", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_Id1" });
            CreateTable(
                "dbo.CourseApplicationUsers",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CourseApplicationUser1",
                c => new
                    {
                        Course_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Course_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Course_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Courses", "ApplicationUser_Id");
            DropColumn("dbo.Courses", "ApplicationUser_Id1");
            DropColumn("dbo.AspNetUsers", "Course_Id");
            DropColumn("dbo.AspNetUsers", "Course_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Course_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Course_Id", c => c.Int());
            AddColumn("dbo.Courses", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.CourseApplicationUser1", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseApplicationUser1", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseApplicationUsers", "Course_Id", "dbo.Courses");
            DropIndex("dbo.CourseApplicationUser1", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CourseApplicationUser1", new[] { "Course_Id" });
            DropIndex("dbo.CourseApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CourseApplicationUsers", new[] { "Course_Id" });
            DropTable("dbo.CourseApplicationUser1");
            DropTable("dbo.CourseApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "Course_Id1");
            CreateIndex("dbo.AspNetUsers", "Course_Id");
            CreateIndex("dbo.Courses", "ApplicationUser_Id1");
            CreateIndex("dbo.Courses", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Course_Id1", "dbo.Courses", "Id");
            AddForeignKey("dbo.AspNetUsers", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Courses", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
