using AutoMapper;
using OnlineStore_BLL.DTO;
using OnlineStore_BLL.Exstensions;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<ProductImage, Product>()
                    .ForPath(p => p.Image.Url,
                        opt => opt.MapFrom(pi => pi.ImageURL))
                    .ForPath(p => p.Image.Name,
                        opt => opt.MapFrom(pi => pi.Name))
                    .ForMember(p => p.CategoryId,
                        opt => opt.MapFrom(pi => pi.Category.Id));
            });
            _mapper = new Mapper(config);
        }

        public async Task CreateAsync(ProductImage productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException("Can't create empty product");

            if ((await _unitOfWork.ProductRepository.GetAllAsync())
                .FirstOrDefault(p => p.Name == productDto.Name) != null)
                throw new ArgumentException("Database allready contains product with such name");

            var productToAdd = _mapper.Map<ProductImage, Product>(productDto);

            if (productToAdd.Category != null)
                productToAdd.Category.Products.Add(productToAdd);

            await _unitOfWork.ProductRepository.AddAsync(productToAdd);
        }

        public async Task UpdateAsync(ProductImage productDTO)
        {
            if (productDTO == null)
                throw new ArgumentNullException("Can't update empty product");

            var productToBeUpdated = (await _unitOfWork.ProductRepository.GetAllAsync())
                .FirstOrDefault(p => p.Id == productDTO.Id);

            if ((await _unitOfWork.ProductRepository.GetAllAsync())
                .FirstOrDefault(p => p.Name == productDTO.Name) != null)
                throw new ArgumentException("Database allready contains product with such name");

            _mapper.Map(productDTO, productToBeUpdated);

            if (productToBeUpdated.Category != null &&
                productToBeUpdated.Category.Products.FirstOrDefault(p => p.Id == productToBeUpdated.Id) == null)
                productToBeUpdated.Category.Products.Add(productToBeUpdated);

            await _unitOfWork.ProductRepository.UpdateAsync(productToBeUpdated);
        }

        public async Task DeleteAsync(Product entity)
        {
            if (!(await _unitOfWork.ProductRepository.GetAllAsync()).Contains(entity))
                throw new ArgumentException("There is no such product you are trying to delete");

            await _unitOfWork.ProductRepository.DeleteAsync(entity.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (await _unitOfWork.ProductRepository.GetAsync(id) == null)
                throw new ArgumentException($"There is no product with such id \"{id}\"");

            await _unitOfWork.ProductRepository.DeleteAsync(id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var productToReturn = await _unitOfWork.ProductRepository.GetAsync(id);
            if (productToReturn == null)
                throw new ArgumentException($"There is no such product with id \"{id}\"");

            return productToReturn;
        }

        public async Task<IEnumerable<Product>> GetShuffledProductsAsync()
        {
            var rng = new Random();

            var productsToShuffle = (await _unitOfWork.ProductRepository.GetAllAsync()).ToList();

            if (productsToShuffle == null)
                throw new ArgumentException("There is no products in database");

            productsToShuffle.Shuffle(rng);

            return productsToShuffle;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchString)
        {
            var productList = new List<Product>();

            var allProduct = await _unitOfWork.ProductRepository.GetAllAsync();

            productList.AddRange(allProduct
                .Where(p => p.Name.Contains(searchString)
                            || p.Price.ToString().Contains(searchString)
                            || p.Category.Name.Contains(searchString)));

            return productList;
        }
    }
}