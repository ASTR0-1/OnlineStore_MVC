using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> SignIn(SignIn entity)
        {
            var user = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Email == entity.Email);
            
            if (user == null)
                throw new ArgumentException($"There is no user with such email \"{entity.Email}\"");

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, entity.Password);

            if (!isPasswordCorrect)
                throw new ArgumentException($"Wrong password for user \"{user.Email}\"");

            await _signInManager.SignInAsync(user, true);

            return user;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task SignUp(SignUp entity)
        {
            var user = new User
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                UserName = entity.Email,
                ShoppingCart = new ShoppingCart { Products = new List<Product>() },
                WishList = new WishList { Products = new List<Product>() } ,
                Receipts = new List<Receipt>()
            };

            var result = await _userManager.CreateAsync(user, entity.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(';', result.Errors.Select(ie => ie.Description)));

            var currentUser = await _userManager.Users
                .Include(u => u.WishList)
                .Include(u => u.Receipts)
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Email == entity.Email); ;

            await _userManager.AddToRoleAsync(currentUser, "Customer");
            await _signInManager.SignInAsync(user, true);
        }

        public async Task<Message> ForgotPassword(ForgotPassword entity)
        {
            var user = await _userManager.FindByEmailAsync(entity.Email);

            if (user == null)
                throw new ArgumentException($"There is no user with such email \"{entity.Email}\"");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", entity.Email }
            };
            var callback = QueryHelpers.AddQueryString(entity.ClientURI, param);

            var messageToReturn = new Message(new[] { user.Email }, "Reset password",
                $"Your reset password link: {callback}");

            return messageToReturn;
        }

        public async Task<string> ResetPassword(ResetPassword entity)
        {
            var user = await _userManager.FindByEmailAsync(entity.Email);

            if (user == null)
                throw new ArgumentException($"There is no user with su fch email \"{entity.Email}\"");

            var result = await _userManager.ResetPasswordAsync(user, entity.Token, entity.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(';', result.Errors.Select(ie => ie.Description)));

            return new string("Password successfully reset");
        }
    }
}