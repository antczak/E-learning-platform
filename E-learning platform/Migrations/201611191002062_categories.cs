namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "ImagePath", c => c.String());
            AddColumn("dbo.Courses", "isVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "Category_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Category_Id");
            AddForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Category_Id", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "Category_Id" });
            DropColumn("dbo.Courses", "Category_Id");
            DropColumn("dbo.Courses", "isVisible");
            DropColumn("dbo.Courses", "ImagePath");
            DropTable("dbo.CourseCategories");
        }
    }
}
