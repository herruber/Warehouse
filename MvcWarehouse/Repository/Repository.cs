using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWarehouse.Repository
{
    public class Repository
    {
        DataAccess.StoreContext sC = new DataAccess.StoreContext();

        public IEnumerable<Models.StockItem> Stock()
        {
            var stock = from i in sC.Items
                        orderby i.Name ascending
                        select i;
            return stock;
        }

        public IEnumerable<Models.StockItem> Search(string name, int[] options)
        {
            string nlower = name.ToLower();

            var fetchedItems = (IEnumerable<Models.StockItem>)sC.Items;

            if (options[options.Length - 1] == 1) // If search all fields is true
            {
                fetchedItems = 
                fetchedItems.Where(e => e.Name.ToLower().Contains(nlower)
                    || e.Description.ToLower().Contains(nlower)
                    || e.Price.ToString().Contains(name)
                    || e.Quantity.ToString().Contains(name)
                    || e.ShelfPosition.ToString().Contains(name)
                    || e.ArticleNumber.ToString().Contains(name));
            }
            else //Else search for each option
            {
                for (int i = 0; i < options.Length; i++)
                {

                    if (options[i] == 1) //If current option name/price/articlenumber/all is checked 1
                    {

                        switch (i) //Switch search based on option
                        {
                            case 0:
                                fetchedItems = sC.Items.Where(f => f.Name.ToLower().Contains(nlower));
                                break;
                            case 1:
                                fetchedItems = sC.Items.Where(g => g.Price.ToString().Contains(name));
                                break;
                            case 2:
                                fetchedItems = sC.Items.Where(h => h.ArticleNumber.ToString().Contains(name));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }          

            return fetchedItems;
        }

        public void AddItem(Models.StockItem item)
        {
            sC.Items.Add(item);
            sC.SaveChanges();
        }

        public Models.StockItem Find(int id)
        {
            return sC.Items.Find(id);
        }

        public void Edit(Models.StockItem item)
        {
            sC.Entry(item).State = System.Data.Entity.EntityState.Modified;
            sC.SaveChanges();
        }

        public void Remove(int id)
        {
            var item = sC.Items.FirstOrDefault(i => i.ArticleNumber == id);
            sC.Items.Remove(item);
            sC.SaveChanges();
        }

        public User.ShopUser GetUser()
        {
            string email = "";

            if (HttpContext.Current.Session["user"].ToString() != null && HttpContext.Current.Session["user"].ToString() != "")
            {
                email = HttpContext.Current.Session["user"].ToString();
            }
            

            return sC.Users.FirstOrDefault(e => e.Email == email);

        }

        public void Buy(int id)
        {
            var cuser = GetUser();

            if (sC.Items.FirstOrDefault(e => e.ArticleNumber == id).Quantity > 0) //If the item quantity is greater than 0
            {

                if (HttpContext.Current.Session["user"] != null && (User.ShopUser.UserType)HttpContext.Current.Session["usertype"] != User.ShopUser.UserType.Visitor)
                {

                    cuser.Cart += "#" + id; //# = item separator, ¤ = purchase separation
                    sC.SaveChanges();
                }
            }
                    
                    HttpContext.Current.Session["items"] = CartSize();
                 
        }

        public int CartSize()
        {
            var cuser = GetUser();

            string tempcart = cuser.Cart;

            string[] purchases = tempcart.Split('¤'); //Split purchases, get last one
            string cPurchase = purchases[purchases.Length - 1];
            string[] articles = cPurchase.Split('#');

            articles = articles.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return articles.Length;
        }

        public void RemoveFromCart(int id)
        {

            User.ShopUser cuser = GetUser();

            string idstr = id.ToString();

            string tempcart = cuser.Cart;

            string[] purchases = tempcart.Split('¤'); //Split purchases, get last one
            string cPurchase = purchases[purchases.Length - 1];

            //string[] items = purchases[purchases.Length - 1].Split('#'); //Split for each item

            for (int i = 0; i < cPurchase.Length; i++)
            {
                if (cPurchase[i].ToString() == idstr)
                {
                    tempcart = tempcart.Remove(i, 2); //¤ + cPurchase 

                    cuser.Cart = tempcart;
                    sC.SaveChanges();

                    break;
                }                
            }

            HttpContext.Current.Session["items"] = CartSize();
        }

        public IEnumerable<Models.StockItem> GetCart()
        {
            List<Models.StockItem> temp = new List<Models.StockItem>();

            string[] purchases = GetUser().Cart.Split('¤'); //Split purchases, get last one

            string[] items = purchases[purchases.Length - 1].Split('#'); //Split for each item

            for (int i = 0; i < items.Length; i++)
            {
                int id = -1;

                try
                {
                    id = int.Parse(items[i]);
                    temp.Add(sC.Items.FirstOrDefault(e => e.ArticleNumber == id)); //Parse item i as an int, article number
                }
                catch (Exception ef)
                {

                    Console.WriteLine("The item article id was not parsable to an int: " +ef);
                }
                
            }

            HttpContext.Current.Session["items"] = CartSize();

            return temp;
        }

        public void AddUser(string email, string password)
        {
            User.ShopUser tempuser = new User.ShopUser();
            tempuser.Email = email;
            tempuser.Password = password;
            tempuser.uType = User.ShopUser.UserType.Customer;
            tempuser.Cart = "¤";
            

            sC.Users.Add(tempuser);
            sC.SaveChanges();
        }

        public void Auth(string email, string password)
        {

            var user = sC.Users.FirstOrDefault(i => i.Email == email);


 
            if (user != null)
            {
                if (user.Password.Equals(password))
                {
                    HttpContext context = HttpContext.Current;
                    context.Session["user"] = user.Email;
                    context.Session["items"] = CartSize();
                    context.Session["usertype"] = user.uType; //Set session usertype
                }
                //Auth success
            }
            else
            {
                //
            }
        }
    }
}