namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbfix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "UserQuestionAnswer_Id", "dbo.UserQuestionAnswers");
            DropIndex("dbo.Answers", new[] { "UserQuestionAnswer_Id" });
            CreateTable(
                "dbo.AnswerUserQuestionAnswers",
                c => new
                    {
                        Answer_Id = c.Int(nullable: false),
                        UserQuestionAnswer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Answer_Id, t.UserQuestionAnswer_Id })
                .ForeignKey("dbo.Answers", t => t.Answer_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserQuestionAnswers", t => t.UserQuestionAnswer_Id, cascadeDelete: true)
                .Index(t => t.Answer_Id)
                .Index(t => t.UserQuestionAnswer_Id);
            
            DropColumn("dbo.Answers", "UserQuestionAnswer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "UserQuestionAnswer_Id", c => c.Int());
            DropForeignKey("dbo.AnswerUserQuestionAnswers", "UserQuestionAnswer_Id", "dbo.UserQuestionAnswers");
            DropForeignKey("dbo.AnswerUserQuestionAnswers", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.AnswerUserQuestionAnswers", new[] { "UserQuestionAnswer_Id" });
            DropIndex("dbo.AnswerUserQuestionAnswers", new[] { "Answer_Id" });
            DropTable("dbo.AnswerUserQuestionAnswers");
            CreateIndex("dbo.Answers", "UserQuestionAnswer_Id");
            AddForeignKey("dbo.Answers", "UserQuestionAnswer_Id", "dbo.UserQuestionAnswers", "Id");
        }
    }
}
