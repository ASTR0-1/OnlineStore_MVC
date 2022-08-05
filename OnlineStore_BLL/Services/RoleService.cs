using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore_BLL.DTO.AuthDTO;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignUserToRoles(AddRoleToUser addRoleToUser)
        {
            var userToChange = await _userManager.FindByEmailAsync(addRoleToUser.Email);

            if (userToChange == null)
                throw new ArgumentException($"User with email \"{addRoleToUser.Email}\" doesn't exists");

            var roles = _roleManager.Roles
                .Where(r => addRoleToUser.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(userToChange, roles);

            if (!result.Succeeded)
                throw new Exception(string.Join(';', result.Errors.Select(e => e.Description)));
        }

        public async Task CreateRole(string roleName)
        {
            if (_roleManager.Roles.Select(r => r.NormalizedName).Contains(roleName.ToUpper()))
                throw new ArgumentException($"Role with name \"{roleName}\" already exsists");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
                throw new Exception($"Error while creating role \"{roleName}\"");
        }

        public async Task<IEnumerable<string>> GetRoles(string email)
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ArgumentException($"User with email \"{email}\" doesn't exists");

            return await _userManager.GetRolesAsync(user);
        }
    }
}