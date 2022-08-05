using System.ComponentModel.DataAnnotations;

namespace OnlineStore_BLL.DTO.AuthDTO
{
    public class AddRoleToUser
    {
        [Required][EmailAddress] public string Email { get; set; }

        [Required] public string[] Roles { get; set; }
    }
}