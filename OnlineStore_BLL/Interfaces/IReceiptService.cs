using OnlineStore_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Interfaces
{
    public interface IReceiptService : IGenericService<Receipt>
    {
        Task<IEnumerable<Receipt>> GetAllAsync();

        Task<IEnumerable<Receipt>> GetAllUserReceiptsAsync(User entity);
    }
}