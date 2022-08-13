using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_BLL.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore_PL.ViewComponents
{
    [Authorize]
    public class WishList : ViewComponent
    {
        private readonly IAdministrationUnitOfWork _administrationUnitOfWork;
        private readonly IWishListService _wishListService;

        public WishList(IAdministrationUnitOfWork administrationUnitOfWork, IWishListService wishListService)
        {
            _administrationUnitOfWork = administrationUnitOfWork;
            _wishListService = wishListService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(User.Identity.Name);
            var products = await _wishListService.GetWishedProductsAsync(user.Id);

            return View("WishList", products);
        }

    }
}
