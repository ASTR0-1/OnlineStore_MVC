using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineStore_DAL.Models;

namespace OnlineStore_PL.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager, RoleData adminInfo)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));

            if (await roleManager.FindByNameAsync("Customer") == null)
                await roleManager.CreateAsync(new IdentityRole<int>("Customer"));

            if (await userManager.FindByNameAsync(adminInfo.Email) == null)
            {
                var admin = new User
                {
                    FirstName = "Administrator", LastName = "ADM", Email = adminInfo.Email, UserName = adminInfo.Email,
                    Receipts = new List<Receipt>(), WishList = new WishList(), ShoppingCart = new ShoppingCart()
                };

                var result = await userManager.CreateAsync(admin, adminInfo.Password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}