using OnlineStore_BLL.DTO;
using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IProductService : IGenericService<Product>
    {
        Task CreateAsync(ProductImage productDto);

        Task UpdateAsync(ProductImage productDto);

        Task<IEnumerable<Product>> GetShuffledProductsAsync();

        Task<IEnumerable<Product>> SearchProductsAsync(string searchString);
    }
}