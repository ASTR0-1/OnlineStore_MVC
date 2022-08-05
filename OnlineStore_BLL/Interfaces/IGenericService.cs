using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task DeleteAsync(T entity);

        Task DeleteByIdAsync(int id);
    }
}