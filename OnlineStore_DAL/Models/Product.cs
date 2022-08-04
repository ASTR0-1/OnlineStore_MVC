using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineStore_DAL.CustomValidation;

namespace OnlineStore_DAL.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [CheckCapitalized]
        public string Name { get; set; }

        [MaxLength(50)] public string Description { get; set; }

        [Required]
        [Range(1, 10000)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Available")]
        [Range(1, 1000)]
        public int AmountAvailable { get; set; }

        [Required] public List<WishList> WishLists { get; set; }

        [Required] public List<Receipt> Receipts { get; set; }

        [Required] public List<ShoppingCart> ShoppingCarts { get; set; }

        public Image Image { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")] public Category Category { get; set; }
    }
}