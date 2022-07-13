using System.ComponentModel.DataAnnotations;

namespace OnlineStore_DAL.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}