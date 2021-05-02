namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.CFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        Path = c.String(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            DropColumn("dbo.Questions", "Correct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Correct", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.CFiles", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.CFiles", new[] { "Course_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropTable("dbo.CFiles");
            DropTable("dbo.Answers");
        }
    }
}
