namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuestionAnswers", newName: "UserQuestionAnswers");
            RenameColumn(table: "dbo.Answers", name: "QuestionAnswer_Id", newName: "UserQuestionAnswer_Id");
            RenameIndex(table: "dbo.Answers", name: "IX_QuestionAnswer_Id", newName: "IX_UserQuestionAnswer_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Answers", name: "IX_UserQuestionAnswer_Id", newName: "IX_QuestionAnswer_Id");
            RenameColumn(table: "dbo.Answers", name: "UserQuestionAnswer_Id", newName: "QuestionAnswer_Id");
            RenameTable(name: "dbo.UserQuestionAnswers", newName: "QuestionAnswers");
        }
    }
}
