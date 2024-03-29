﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ShoppingCartService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task AddProductAsync(int userId, int productId)
        {
            var user = await _userManager.Users
                .Include(u => u.WishList)
                    .ThenInclude(wl => wl.Products)

                .Include(u => u.Receipts)
                    .ThenInclude(r => r.Products)

                .Include(u => u.ShoppingCart)
                    .ThenInclude(sc => sc.Products)

                .FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));
            var product = await _unitOfWork.ProductRepository.GetAsync(productId);

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            if (product == null)
                throw new ArgumentException($"There is no such Product with this id \"{productId}\"");

            var shoppingCart = user.ShoppingCart;
            shoppingCart.Products.Add(product);

            await _userManager.UpdateAsync(user);
            await _unitOfWork.ShoppingCartRepository.UpdateAsync(shoppingCart);
        }

        public async Task RemoveProductAsync(int userId, int productId)
        {
            var user = await _userManager.Users
                 .Include(u => u.WishList)
                     .ThenInclude(wl => wl.Products)

                 .Include(u => u.Receipts)
                     .ThenInclude(r => r.Products)

                 .Include(u => u.ShoppingCart)
                     .ThenInclude(sc => sc.Products)

                 .FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));
            var product = await _unitOfWork.ProductRepository.GetAsync(productId);

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            if (product == null)
                throw new ArgumentException($"There is no such Product with this id \"{productId}\"");

            var shoppingCart = user.ShoppingCart;
            shoppingCart.Products.Remove(product);

            await _userManager.UpdateAsync(user);
            await _unitOfWork.ShoppingCartRepository.UpdateAsync(shoppingCart);
        }

        public async Task ClearAsync(int userId)
        {
            var user = await _userManager.Users
                .Include(u => u.WishList)
                    .ThenInclude(wl => wl.Products)

                .Include(u => u.Receipts)
                    .ThenInclude(r => r.Products)

                .Include(u => u.ShoppingCart)
                    .ThenInclude(sc => sc.Products)

                .FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            var userShoppingCart = user.ShoppingCart;
            userShoppingCart.Products.Clear();

            await _unitOfWork.ShoppingCartRepository.UpdateAsync(userShoppingCart);
        }

        public async Task DeleteAsync(ShoppingCart entity)
        {
            var shoppingCartToDelete = _unitOfWork.ShoppingCartRepository.GetAsync(entity.Id);

            if (shoppingCartToDelete == null)
                throw new ArgumentException($"There is no ShoppingCart with this id \"{entity.Id}\"");

            await _unitOfWork.ShoppingCartRepository.DeleteAsync(entity.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var shoppingCartToDelete = await _unitOfWork.ShoppingCartRepository.GetAsync(id);

            if (shoppingCartToDelete == null)
                throw new ArgumentException($"There is no ShoppingCart with this id \"{id}\"");

            await _unitOfWork.ShoppingCartRepository.DeleteAsync(id);
        }

        public Task<ShoppingCart> GetByIdAsync(int id)
        {
            var shoppingCart = _unitOfWork.ShoppingCartRepository.GetAsync(id);

            if (shoppingCart == null)
                throw new ArgumentException($"There is no ShoppingCart with this id \"{id}\"");

            return shoppingCart;
        }

        public async Task<IEnumerable<Product>> GetCartProductsAsync(int userId)
        {
            var user = await _userManager.Users
                .Include(u => u.WishList)
                    .ThenInclude(wl => wl.Products)

                .Include(u => u.Receipts)
                    .ThenInclude(r => r.Products)

                .Include(u => u.ShoppingCart)
                    .ThenInclude(sc => sc.Products)

                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            var productsToReturn = user.ShoppingCart.Products;

            return productsToReturn;
        }

        public async Task Checkout(int userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Receipts)
                    .ThenInclude(r => r.Products)

                .Include(u => u.ShoppingCart)
                    .ThenInclude(sc => sc.Products)

                .FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            var userReceipts = user.Receipts;

            userReceipts.Add(new Receipt
            {
                User = user,
                UserId = userId,
                Address = user.Address,
                City = user.City,
                Products = (await GetCartProductsAsync(userId)).ToList(),
                Date = DateTime.Now
            });

            await _userManager.UpdateAsync(user);
            await ClearAsync(userId);
        }
    }
}