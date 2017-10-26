using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWarehouse.User
{
    public class ShopUser
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cart { get; set; }
        
        public enum UserType
        {
            Customer,
            Admin
        }

        public UserType uType { get; set; }

        public ShopUser()
        {

        }

        public ShopUser(string email, string password, string cart, UserType utype)
        {
            Email = email;
            Password = password;
            uType = utype;
            Cart = cart;
        }

    }
}