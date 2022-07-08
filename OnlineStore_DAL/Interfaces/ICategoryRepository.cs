using OnlineStore_DAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineStore_DAL.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> GetCategoryByIdAsync(int id);

        public Task<Category> GetCategoryByNameAsync(string name);

        public Task<List<Product>> GetAllProductsByCategoryIdAsync(int id);
    }
}
