using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore_BLL.DTO;
using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Models;
using OnlineStore_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdministrationController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();

            int pageSize = 10;
            IEnumerable<Product> list = (await _productService.SearchProductsAsync("")).ToList();
            var count = list.Count();
            var items = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<Product> viewModel = new IndexViewModel<Product>
            {
                PageViewModel = pageViewModel,
                Values = items,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            var selectedCategory = await _categoryService.GetByIdAsync(Convert.ToInt32(product.Category.Id));
            var selectedCategoryName = selectedCategory.Name;

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;

            var selectList = new SelectList(categories.ToList(), "Id", "Name");
            var selected = selectList.Where(x => x.Text == $"{selectedCategoryName}").First();
            selected.Selected = true;

            ViewBag.CategorySelect = selectList;

            Task.WaitAll();

            ProductImage productToReturn = new ProductImage
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                AmountAvailable = product.AmountAvailable,
                ImageURL = product.Image.Url,
                Category = product.Category
            };

            return View(productToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductImage product)
        {
            try
            {
                await _productService.UpdateAsync(product);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }

            return View(product);
        }
    }
}
