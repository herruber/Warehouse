namespace MvcWarehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShopUsers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShopUsers", "Password", c => c.Binary());
        }
    }
}
