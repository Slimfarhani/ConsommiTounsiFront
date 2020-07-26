namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
