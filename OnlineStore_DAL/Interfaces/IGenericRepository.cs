using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllAsync();

        Task<T> GetAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
