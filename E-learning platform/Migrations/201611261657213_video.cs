namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class video : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        Order = c.Int(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropTable("dbo.Videos");
        }
    }
}
