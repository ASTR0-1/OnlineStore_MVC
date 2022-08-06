namespace OnlineStore_BLL.Interfaces
{
    public interface IAdministrationUnitOfWork
    {
        IUserService UserService { get; }
        IRoleService RoleService { get; }
        IAuthService AuthService { get; }
    }
}
