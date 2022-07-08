using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteByIdAsync(int id);
    }
}
