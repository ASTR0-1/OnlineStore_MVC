using Microsoft.Extensions.DependencyInjection;
using OnlineStore_BLL.Interfaces;
using OnlineStore_BLL.Services;

namespace OnlineStore_PL.ServiceExstensions
{
    public static class ServiceExstensions
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IWishListService, WishListService>();
        }
    }
}
