namespace MvcWarehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        ArticleNumber = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        ShelfPosition = c.String(),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ArticleNumber);
            
            CreateTable(
                "dbo.ShopUsers",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.Binary(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopUsers");
            DropTable("dbo.StockItems");
        }
    }
}
