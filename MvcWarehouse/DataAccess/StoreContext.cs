using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcWarehouse.DataAccess
{


    public class StoreContext : DbContext
    {

        public DbSet<Models.StockItem> Items { get; set; }
        public DbSet<User.ShopUser> Users { get; set; }

        public StoreContext() : base("DefaultConnection")
        {
        
        }

    }
}