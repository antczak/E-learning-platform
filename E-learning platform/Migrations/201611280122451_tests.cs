namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "QuestionLimit", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "TestResult_Id", c => c.Int());
            AddColumn("dbo.TestResults", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "TestResult_Id");
            CreateIndex("dbo.TestResults", "User_Id");
            AddForeignKey("dbo.Questions", "TestResult_Id", "dbo.TestResults", "Id");
            AddForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "TestResult_Id", "dbo.TestResults");
            DropIndex("dbo.TestResults", new[] { "User_Id" });
            DropIndex("dbo.Questions", new[] { "TestResult_Id" });
            DropColumn("dbo.TestResults", "User_Id");
            DropColumn("dbo.Questions", "TestResult_Id");
            DropColumn("dbo.Tests", "QuestionLimit");
        }
    }
}
