namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderitem1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.OrderItems", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Total", c => c.Single(nullable: false));
            DropColumn("dbo.OrderItems", "Price");
        }
    }
}
