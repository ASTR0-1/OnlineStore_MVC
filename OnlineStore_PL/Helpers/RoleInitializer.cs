using Microsoft.AspNetCore.Identity;
using OnlineStore_DAL.Models;
using System.Threading.Tasks;

namespace OnlineStore_PL.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, RoleData adminInfo)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));

            if (await roleManager.FindByNameAsync("Customer") == null)
                await roleManager.CreateAsync(new IdentityRole<int>("Customer"));

            if (await userManager.FindByNameAsync(adminInfo.Email) == null)
            {
                User admin = new User { FirstName = "Administrator", LastName = "ADM", Email = adminInfo.Email, UserName = adminInfo.Email };

                IdentityResult result = await userManager.CreateAsync(admin, adminInfo.Password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
