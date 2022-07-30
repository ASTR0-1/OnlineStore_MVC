using Microsoft.AspNetCore.Identity;
using OnlineStore_DAL.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore_DAL.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [CheckCapitalized]
        public string FirstName { get; set; }

        [Required]
        [CheckCapitalized]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public WishList WishList { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public Receipt Receipt { get; set; }
    }
}