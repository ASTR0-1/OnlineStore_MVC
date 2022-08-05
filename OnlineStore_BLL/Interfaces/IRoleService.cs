using OnlineStore_BLL.DTO.AuthDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IRoleService
    {
        Task AssignUserToRoles(AddRoleToUser addRoleToUser);

        Task CreateRole(string roleName);

        Task<IEnumerable<string>> GetUserRoles(string email);

        Task<IEnumerable<string>> GetRoles(string email);
    }
}