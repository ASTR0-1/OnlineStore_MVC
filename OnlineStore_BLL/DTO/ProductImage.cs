using OnlineStore_DAL.CustomValidation;
using OnlineStore_DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore_BLL.DTO
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [CheckCapitalized]
        public string Name { get; set; }

        [MaxLength(20)] public string Description { get; set; }

        [Required]
        [Range(1, 10000)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [DisplayName("Available")]
        [Range(1, 1000)]
        public int AmountAvailable { get; set; }

        public string ImageURL { get; set; }

        public Category Category { get; set; }
    }
}