using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_BLL.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore_PL.ViewComponents
{
    [Authorize]
    public class Receipts : ViewComponent
    {
        private readonly IAdministrationUnitOfWork _administrationUnitOfWork;
        private readonly IReceiptService _receiptService;

        public Receipts(IAdministrationUnitOfWork administrationUnitOfWork, IReceiptService receiptService)
        {
            _administrationUnitOfWork = administrationUnitOfWork;
            _receiptService = receiptService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(User.Identity.Name);
            var receipts = await _receiptService.GetAllUserReceiptsAsync(user);

            return View("Receipts", receipts);
        }
    }
}
