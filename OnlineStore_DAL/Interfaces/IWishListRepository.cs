using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IWishListRepository : IGenericRepository<WishList>
    {
        public Task<WishList> GetWishListByIdAsync(int id);
        public Task<WishList> GetWishListByUserIdAsync(int userId);

        public Task<int> GetWishListIdByUserAsync(User user);

        public Task<Product> GetProductByIdAsync(int productId);

        public Task<List<Product>> GetAllProductsByWishListId(int wishListId);
        public Task<List<Product>> GetAllProductsByUserId(int userId);
    }
}
