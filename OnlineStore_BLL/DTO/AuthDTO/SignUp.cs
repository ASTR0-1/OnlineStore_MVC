using OnlineStore_DAL.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class SignUp
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required][EmailAddress] public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password should contain more than 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords aren't equal")]
        public string ConfirmPassword { get; set; }
    }
}