namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class balance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Courses", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.AspNetUsers", "Balance");
        }
    }
}
