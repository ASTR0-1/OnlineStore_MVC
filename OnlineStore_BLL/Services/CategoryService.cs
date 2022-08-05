using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Category entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Can't add empty category");

            await _unitOfWork.CategoryRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Category entity)
        {
            await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            if (!(await _unitOfWork.CategoryRepository.GetAllAsync()).Contains(entity))
                throw new ArgumentException($"There is no category with such name \"{entity.Name}\"");

            await _unitOfWork.CategoryRepository.DeleteAsync(entity.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (await _unitOfWork.CategoryRepository.GetAsync(id) == null)
                throw new ArgumentException($"There is no such category with id \"{id}\"");

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryToReturn = await _unitOfWork.CategoryRepository.GetAsync(id);
            if (categoryToReturn == null)
                throw new ArgumentException($"There is no such category with id \"{id}\"");

            return categoryToReturn;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }
    }
}