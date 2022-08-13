using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        [Required]
        public string ClientURI { get; set; }
    }
}