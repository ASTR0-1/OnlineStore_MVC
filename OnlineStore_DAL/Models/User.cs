using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OnlineStore_DAL.CustomValidation;

namespace OnlineStore_DAL.Models
{
    public class User : IdentityUser<int>
    {
        [Required] [CheckCapitalized] public string FirstName { get; set; }

        [Required] [CheckCapitalized] public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public WishList WishList { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public List<Receipt> Receipts { get; set; }
    }
}