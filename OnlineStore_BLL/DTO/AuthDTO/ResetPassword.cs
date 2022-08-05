using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class ResetPassword
    {
        [Required][EmailAddress] public string Email { get; set; }

        [Required] public string Token { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password should contain more than 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords aren't equal")]
        public string ConfirmPassword { get; set; }
    }
}