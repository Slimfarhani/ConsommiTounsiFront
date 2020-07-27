namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        SupplierId = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.OrderItemId);
            
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        StockItemId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.StockItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockItems");
            DropTable("dbo.OrderItems");
        }
    }
}
