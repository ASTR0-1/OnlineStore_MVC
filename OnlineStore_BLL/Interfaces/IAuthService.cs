using EmailService;
using OnlineStore_BLL.DTO.AuthDTO;
using OnlineStore_DAL.Models;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IAuthService
    {
        Task SignUp(SignUp entity);

        Task<User> SignIn(SignIn entity);

        Task SignOut();

        Task<Message> ForgotPassword(ForgotPassword entity);

        Task<string> ResetPassword(ResetPassword entity);
    }
}