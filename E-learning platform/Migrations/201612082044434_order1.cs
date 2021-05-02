namespace E_learning_platform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Order");
        }
    }
}
