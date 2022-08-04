using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;

namespace OnlineStore_Tests.DAL_Tests.CategoryRepo
{
    public class CategorySeedDataFixture : IDisposable
    {
        public CategorySeedDataFixture()
        {
            ApplicationDbContext.Categories.Add(new Category
            {
                Name = "Fruits",
                Products = new List<Product>
                {
                    new Product {Name = "Banana", Price = new decimal(10.05), AmountAvailable = 2, CategoryId = 1},
                    new Product {Name = "Apple", Price = new decimal(15.50), AmountAvailable = 5, CategoryId = 1}
                }
            });
            ApplicationDbContext.Categories.Add(new Category
            {
                Name = "Vegetables",
                Products = new List<Product>
                {
                    new Product {Name = "Onion", Price = new decimal(2.45), AmountAvailable = 20, CategoryId = 2},
                    new Product {Name = "Tomato", Price = new decimal(3.50), AmountAvailable = 3, CategoryId = 2}
                }
            });
            ApplicationDbContext.SaveChanges();
        }

        public ApplicationDbContext ApplicationDbContext { get; } = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public void Dispose()
        {
            ApplicationDbContext.Database.EnsureDeleted();
            ApplicationDbContext.Dispose();
        }
    }
}