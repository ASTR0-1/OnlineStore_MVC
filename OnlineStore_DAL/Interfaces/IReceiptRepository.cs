using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        public Task<Receipt> GetReceiptByIdAsync(int id);
        public Task<Receipt> GetReceiptByUserIdAsync(int userId);

        public Task<Product> GetProductByIdAsync(int productId);

        public Task<List<Product>> GetAllProductsByReceiptId(int wishListId);
        public Task<List<Product>> GetAllProductsByUserId(int userId);
    }
}
