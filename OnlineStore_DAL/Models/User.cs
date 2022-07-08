using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore_DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public WishList WishList { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public Receipt Receipt { get; set; }
    }
}