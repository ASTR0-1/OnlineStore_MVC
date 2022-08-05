using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class SignIn
    {
        [EmailAddress] public string Email { get; set; }

        [DataType(DataType.Password)] public string Password { get; set; }
    }
}