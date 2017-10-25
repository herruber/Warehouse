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

        public void AddUser(string email, byte[] password)
        {
            User.ShopUser tempuser = new User.ShopUser();
            tempuser.Email = email;
            tempuser.Password = password;
            tempuser.uType = User.ShopUser.UserType.Customer;

            sC.Users.Add(tempuser);
            sC.SaveChanges();
        }

        public void Auth(string email, byte[] password)
        {

            var user = sC.Users.FirstOrDefault(i => i.Email == email); //Compares two arrays


            if (Enumerable.SequenceEqual(user.Password, password))
            {

            }

            if (user != null)
            {
                //Auth success
            }
            else
            {
                //
            }
        }
    }
}