using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class CreateRole
    {
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string RoleName { get; set; }
    }
}