namespace MvcWarehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopUsers", "uType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopUsers", "uType");
        }
    }
}
