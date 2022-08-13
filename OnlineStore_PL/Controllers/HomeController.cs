using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Models;
using OnlineStore_PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IAdministrationUnitOfWork _administrationUnitOfWork;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWishListService _wishListService;

        public HomeController(ICategoryService categoryService, IProductService productService, IAdministrationUnitOfWork administrationUnitOfWork,
            IShoppingCartService shoppingCartService, IWishListService wishListService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _administrationUnitOfWork = administrationUnitOfWork;
            _shoppingCartService = shoppingCartService;
            _wishListService = wishListService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.IsSearched = false;

            int page = 1;
            int pageSize = 9;
            IEnumerable<Product> list = (await _productService.GetShuffledProductsAsync()).ToList();
            var count = list.Count();
            var items = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<Product> viewModel = new IndexViewModel<Product>
            {
                PageViewModel = pageViewModel,
                Values = items,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Description(int productId)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();

            if (productId < 1)
                return RedirectToAction("Index", "Home");

            var productToReturn = await _productService.GetByIdAsync(productId);

            return View("Description", productToReturn);
        }

        public async Task<IActionResult> Search(int page = 1, string searchString = "")
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.IsSearched = true;
            ViewBag.SearchString = searchString;

            int pageSize = 9;
            IEnumerable<Product> list = (await _productService.SearchProductsAsync(searchString)).ToList();
            var count = list.Count();
            var items = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<Product> viewModel = new IndexViewModel<Product>
            {
                PageViewModel = pageViewModel,
                Values = items,
            };

            return View("Index", viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _shoppingCartService.AddProductAsync(user.Id, productId);
            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int productId, string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _shoppingCartService.RemoveProductAsync(user.Id, productId);

            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> AddToWishList(int productId, string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _wishListService.AddProductAsync(user.Id, productId);

            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromWishList(int productId, string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _wishListService.RemoveProductAsync(user.Id, productId);

            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> ClearShoppingCart(string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _shoppingCartService.ClearAsync(user.Id);

            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> ClearWishList(string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            await _wishListService.ClearAsync(user.Id);

            return Redirect(callBackPath);
        }

        [Authorize]
        public async Task<IActionResult> Checkout(string callBackPath)
        {
            var userEmail = User.Identity.Name;
            var user = await _administrationUnitOfWork.UserService.GetCurrentUser(userEmail);

            if (string.IsNullOrEmpty(user.Address) || string.IsNullOrEmpty(user.City) || string.IsNullOrEmpty(user.PhoneNumber))
            {
                TempData["ErrorPage"] = "To checkout you need to enter all related data!";
                return RedirectToAction("Index", "Account");
            }

            if (user.ShoppingCart.Products == null)
                await _shoppingCartService.Checkout(user.Id);

            return Redirect(callBackPath);
        }
    }
}