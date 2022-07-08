using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        public Task<ShoppingCart> GetCartByIdAsync(int id);

        public Task<List<Product>> GetAllProductsInCartByIdAsync(int id);

        public Task<int> GetUserIdByCartAsync(ShoppingCart shoppingCart);
        public Task<int> GetUserIdByCartIdAsync(int id);
    }
}
