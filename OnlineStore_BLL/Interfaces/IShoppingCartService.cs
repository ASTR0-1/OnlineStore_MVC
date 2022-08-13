using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IShoppingCartService : IGenericService<ShoppingCart>
    {
        Task AddProductAsync(int userId, int productId);

        Task RemoveProductAsync(int userId, int productId);

        Task<IEnumerable<Product>> GetCartProductsAsync(int userId);

        Task ClearAsync(int userId);

        Task Checkout(int userId);
    }
}