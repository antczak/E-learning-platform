namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "FullDesc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "FullDesc");
        }
    }
}
