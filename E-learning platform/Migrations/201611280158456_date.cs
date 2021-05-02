namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.TestResults", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.TestResults", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestResults", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.TestResults", "Date");
            DropColumn("dbo.Announcements", "Date");
        }
    }
}
