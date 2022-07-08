using OnlineStore_DAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineStore_DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<Product> GetProductById(int id);

        public Task<List<WishList>> GetWishListsByIdAsync(int id);
        public Task<List<Receipt>> GetReceiptsByIdAsync(int id);
        public Task<List<ShoppingCart>> GetShoppingCartsByIdAsync(int id);

        public Task<Category> GetCategoryById(int id);
        public Task<Image> GetImageById(int id);
    }
}
