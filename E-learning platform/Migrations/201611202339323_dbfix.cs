namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbfix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Test_Id);
            
            AlterColumn("dbo.Questions", "Correct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "Test_Id", "dbo.Tests");
            DropIndex("dbo.TestResults", new[] { "Test_Id" });
            AlterColumn("dbo.Questions", "Correct", c => c.Int(nullable: false));
            DropTable("dbo.TestResults");
        }
    }
}
