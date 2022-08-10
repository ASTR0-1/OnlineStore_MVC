using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteUser(string email)
        {
            var userToDelete = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (userToDelete == null)
                throw new ArgumentException($"User with email \"{email}\" does not exists");

            var userRoles = await _userManager.GetRolesAsync(userToDelete);

            if (userRoles.Count() > 0)
                foreach (var role in userRoles)
                    await _userManager.RemoveFromRoleAsync(userToDelete, role);

            await _userManager.DeleteAsync(userToDelete);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .ToListAsync();

            return result;
        }

        public async Task<User> GetCurrentUser(string email)
        {
            if (email == null)
                throw new ArgumentException($"User with \"{email}\" not signed in");

            var userToReturn = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (userToReturn == null)
                throw new ArgumentException($"There is no user with such email \"{email}\"");

            return userToReturn;
        }

        public async Task<User> GetUserById(string id)
        {
            var userToReturn = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));

            if (userToReturn == null)
                throw new ArgumentException($"There is no user with such id \"{id}\"");

            return userToReturn;
        }

        public async Task<int> GetUserId(string email)
        {
            var user = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new ArgumentException($"There is no user with such email \"{email}\"");

            var userId = user.Id;

            return userId;
        }

        public async Task UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentException($"There is no user with such email \"{user.Email}\"");

            await _userManager.UpdateAsync(user);
        }
    }
}