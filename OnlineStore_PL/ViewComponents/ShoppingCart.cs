using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_BLL.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore_PL.ViewComponents
{
    [Authorize]
    public class ShoppingCart : ViewComponent
    {
        private readonly IAdministrationUnitOfWork _administrationUnitOfWork;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCart(IAdministrationUnitOfWork administrationUnitOfWork, IShoppingCartService shoppingCartService)
        {
            _administrationUnitOfWork = administrationUnitOfWork;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(User.Identity.Name);
            var products = await _shoppingCartService.GetCartProductsAsync(user.Id);

            return View("ShoppingCart", products);
        }
    }
}
