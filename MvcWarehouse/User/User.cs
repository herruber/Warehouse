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
        public byte[] Password { get; set; }
        
        public enum UserType
        {
            Customer,
            Admin
        }

        public UserType uType { get; set; }

        public Cart cart = new Cart();


    }
}