namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Presentations", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Presentations", "Path");
        }
    }
}
