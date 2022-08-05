using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IUserService
    {
        Task UpdateUser(User user);
        Task DeleteUser(string email);

        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetCurrentUser(string email);
        Task<int> GetUserId(string email);
    }
}