using OnlineStore_DAL.Models;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Interfaces
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        public Task<Image> GetImageByIdAsync(int id);
        public Task<Image> GetImageByNameAsync(string name);
        public Task<Image> GetImageByProductIdAsync(int productId);
    }
}
