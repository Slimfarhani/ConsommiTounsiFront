namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderItems");
            AddColumn("dbo.OrderItems", "UserID", c => c.Long(nullable: false));
            AlterColumn("dbo.OrderItems", "OrderItemId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.OrderItems", "ProductId", c => c.Long(nullable: false));
            AlterColumn("dbo.OrderItems", "SupplierId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.OrderItems", "OrderItemId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderItems");
            AlterColumn("dbo.OrderItems", "SupplierId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "OrderItemId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.OrderItems", "UserID");
            AddPrimaryKey("dbo.OrderItems", "OrderItemId");
        }
    }
}
