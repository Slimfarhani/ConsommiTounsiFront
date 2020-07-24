namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Name", c => c.String());
            AddColumn("dbo.OrderItems", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "ImageUrl");
            DropColumn("dbo.OrderItems", "Name");
        }
    }
}
