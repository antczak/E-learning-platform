namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserQuestionAnswers", "IsCorrect");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserQuestionAnswers", "IsCorrect", c => c.Boolean(nullable: false));
        }
    }
}
