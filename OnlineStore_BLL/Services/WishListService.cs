using Microsoft.AspNetCore.Identity;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public WishListService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task DeleteAsync(WishList entity)
        {
            var wishListToDelete = await _unitOfWork.WishListRepository.GetAsync(entity.Id);

            if (wishListToDelete == null)
                throw new ArgumentException($"There is no such WishList with this id \"{entity.Id}\"");

            await _unitOfWork.WishListRepository.DeleteAsync(wishListToDelete.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var wishListToDelete = await _unitOfWork.WishListRepository.GetAsync(id);

            if (wishListToDelete == null)
                throw new ArgumentException($"There is no such WishList with this id \"{id}\"");

            await _unitOfWork.WishListRepository.DeleteAsync(wishListToDelete.Id);
        }

        public async Task<WishList> GetByIdAsync(int id)
        {
            var wishList = await _unitOfWork.WishListRepository.GetAsync(id);

            if (wishList == null)
                throw new ArgumentException($"There is no such WishList with this id \"{id}\"");

            return wishList;
        }

        public async Task AddProductAsync(int userId, int productId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var product = await _unitOfWork.ProductRepository.GetAsync(productId);

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            if (product == null)
                throw new ArgumentException($"There is no such Product with this id \"{productId}\"");

            var wishList = user.WishList;
            wishList.Products.Add(product);

            await _unitOfWork.WishListRepository.UpdateAsync(wishList);
        }

        public async Task RemoveProductAsync(int userId, int productId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var product = await _unitOfWork.ProductRepository.GetAsync(productId);

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            if (product == null)
                throw new ArgumentException($"There is no such Product with this id \"{product.Id}\"");

            var wishList = user.WishList;
            wishList.Products.Remove(product);

            await _unitOfWork.WishListRepository.UpdateAsync(wishList);
        }

        public async Task<IEnumerable<Product>> GetWishedProductsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new ArgumentException($"There is no such User with this id \"{userId}\"");

            var productsToReturn = user.WishList.Products;

            return productsToReturn;
        }

        public async Task ClearAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new ArgumentException($"There is no such User this id \"{userId}\"");

            var wishList = user.WishList;
            wishList.Products.Clear();

            await _unitOfWork.WishListRepository.UpdateAsync(wishList);
        }
    }
}