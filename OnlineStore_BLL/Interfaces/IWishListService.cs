using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IWishListService : IGenericService<WishList>
    {
        Task AddProductAsync(int userId, int productId);

        Task RemoveProductAsync(int userId, int productId);

        Task<IEnumerable<Product>> GetWishedProductsAsync(int userId);

        Task ClearAsync(int userId);
    }
}