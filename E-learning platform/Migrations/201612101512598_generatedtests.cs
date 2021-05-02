namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class generatedtests : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestResults", newName: "GeneratedTests");
            DropForeignKey("dbo.Questions", "TestResult_Id", "dbo.TestResults");
            DropIndex("dbo.Questions", new[] { "TestResult_Id" });
            CreateTable(
                "dbo.QuestionGeneratedTests",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        GeneratedTest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.GeneratedTest_Id })
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .ForeignKey("dbo.GeneratedTests", t => t.GeneratedTest_Id, cascadeDelete: true)
                .Index(t => t.Question_Id)
                .Index(t => t.GeneratedTest_Id);
            
            AlterColumn("dbo.GeneratedTests", "Score", c => c.Int());
            DropColumn("dbo.Questions", "TestResult_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "TestResult_Id", c => c.Int());
            DropForeignKey("dbo.QuestionGeneratedTests", "GeneratedTest_Id", "dbo.GeneratedTests");
            DropForeignKey("dbo.QuestionGeneratedTests", "Question_Id", "dbo.Questions");
            DropIndex("dbo.QuestionGeneratedTests", new[] { "GeneratedTest_Id" });
            DropIndex("dbo.QuestionGeneratedTests", new[] { "Question_Id" });
            AlterColumn("dbo.GeneratedTests", "Score", c => c.Int(nullable: false));
            DropTable("dbo.QuestionGeneratedTests");
            CreateIndex("dbo.Questions", "TestResult_Id");
            AddForeignKey("dbo.Questions", "TestResult_Id", "dbo.TestResults", "Id");
            RenameTable(name: "dbo.GeneratedTests", newName: "TestResults");
        }
    }
}
