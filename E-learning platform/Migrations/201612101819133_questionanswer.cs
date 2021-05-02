namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionanswer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionGeneratedTests", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionGeneratedTests", "GeneratedTest_Id", "dbo.GeneratedTests");
            DropIndex("dbo.QuestionGeneratedTests", new[] { "Question_Id" });
            DropIndex("dbo.QuestionGeneratedTests", new[] { "GeneratedTest_Id" });
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        GeneratedTest_Id = c.Int(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneratedTests", t => t.GeneratedTest_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.GeneratedTest_Id)
                .Index(t => t.Question_Id);
            
            AddColumn("dbo.Answers", "QuestionAnswer_Id", c => c.Int());
            CreateIndex("dbo.Answers", "QuestionAnswer_Id");
            AddForeignKey("dbo.Answers", "QuestionAnswer_Id", "dbo.QuestionAnswers", "Id");
            DropTable("dbo.QuestionGeneratedTests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionGeneratedTests",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        GeneratedTest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.GeneratedTest_Id });
            
            DropForeignKey("dbo.Answers", "QuestionAnswer_Id", "dbo.QuestionAnswers");
            DropForeignKey("dbo.QuestionAnswers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswers", "GeneratedTest_Id", "dbo.GeneratedTests");
            DropIndex("dbo.Answers", new[] { "QuestionAnswer_Id" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_Id" });
            DropIndex("dbo.QuestionAnswers", new[] { "GeneratedTest_Id" });
            DropColumn("dbo.Answers", "QuestionAnswer_Id");
            DropTable("dbo.QuestionAnswers");
            CreateIndex("dbo.QuestionGeneratedTests", "GeneratedTest_Id");
            CreateIndex("dbo.QuestionGeneratedTests", "Question_Id");
            AddForeignKey("dbo.QuestionGeneratedTests", "GeneratedTest_Id", "dbo.GeneratedTests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionGeneratedTests", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
