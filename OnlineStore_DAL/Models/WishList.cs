using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore_DAL.Models
{
    public class WishList
    {
        public int Id { get; set; }

        [ForeignKey("UserId")] public int UserId { get; set; }

        [Required] public User User { get; set; }

        [Required] public List<Product> Products { get; set; }
    }
}