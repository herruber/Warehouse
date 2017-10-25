using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWarehouse.User
{
    public class ShopUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public enum UserType
        {
            Customer,
            Admin
        }

        UserType usertype = UserType.Customer;

        public Cart cart = new Cart();


    }
}