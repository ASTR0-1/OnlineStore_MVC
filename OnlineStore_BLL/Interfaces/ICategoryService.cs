using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task CreateAsync(Category entity);

        Task UpdateAsync(Category entity);

        Task<IEnumerable<Category>> GetAllAsync();
    }
}