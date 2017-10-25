using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWarehouse.User
{
    public class Cart
    {
        public List<Models.StockItem> cartItems = new List<Models.StockItem>();

        double price { get; set; }

        public Cart()
        {
            price = 0;
        }

        public void RemoveItem(int articlenumber)
        {
            var item = cartItems.FirstOrDefault(e => e.ArticleNumber == articlenumber);
            price -= item.Price;
            cartItems.Remove(item);
        }

        public void AddItem(int articlenumber)
        {
            var item = cartItems.FirstOrDefault(e => e.ArticleNumber == articlenumber);
            price += item.Price;
            cartItems.Add(item);
        }
    }
}