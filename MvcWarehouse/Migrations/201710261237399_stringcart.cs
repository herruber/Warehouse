namespace MvcWarehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopUsers", "Cart", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopUsers", "Cart");
        }
    }
}
