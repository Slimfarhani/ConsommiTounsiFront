namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
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
                        Total = c.Single(nullable: false),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.OrderItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderItems");
        }
    }
}
