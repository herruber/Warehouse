namespace MvcWarehouse.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcWarehouse.DataAccess.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcWarehouse.DataAccess.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Items.AddOrUpdate(
                p => p.Name,
                new Models.StockItem { ArticleNumber = 0, Name = "Hat", Description = "A hat from some awsome boutique", Price = 99, Quantity = 4, ShelfPosition = "r1, 1" },
                new Models.StockItem { ArticleNumber = 1, Name = "Gloves", Description = "Some leather gloves", Price = 41, Quantity = 12, ShelfPosition = "r1, 2" },
                new Models.StockItem { ArticleNumber = 2, Name = "Skirt", Description = "A decently long skirt", Price = 76.99, Quantity = 32, ShelfPosition = "r1, 3" },
                new Models.StockItem { ArticleNumber = 3, Name = "Bermuda Shirt", Description = "Shirt with some palms", Price = 29.99, Quantity = 43, ShelfPosition = "r2, 1" },
                new Models.StockItem { ArticleNumber = 4, Name = "Motorcycle", Description = "Questionable mc", Price = 9999.99, Quantity = 5, ShelfPosition = "r2, 2" },
                new Models.StockItem { ArticleNumber = 5, Name = "Surfboard", Description = "Yellow surfboard", Price = 100, Quantity = 11, ShelfPosition = "r2, 3" },
                new Models.StockItem { ArticleNumber = 6, Name = "Socks 10p", Description = "High quality socks, 10 pack", Price = 10.99, Quantity = 136, ShelfPosition = "r3, 4" },
                new Models.StockItem { ArticleNumber = 7, Name = "Pens 10p", Description = "Pens, 10 pack", Price = 9.99, Quantity = 500, ShelfPosition = "r3, 5" },
                new Models.StockItem { ArticleNumber = 8, Name = "Wooden Clogs", Description = "Some wooden clogs for your needs", Price = 30, Quantity = 100, ShelfPosition = "r3, 6" }
                );

            //context.Users.AddOrUpdate(
                    
            //    );
        }
    }
}
