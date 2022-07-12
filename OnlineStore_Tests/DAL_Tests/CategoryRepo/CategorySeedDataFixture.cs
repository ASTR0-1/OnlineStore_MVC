using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;

namespace OnlineStore_Tests.DAL_Tests.CategoryRepo
{
    public class CategorySeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OnlineStore_MVC")
                .Options);

        public CategorySeedDataFixture()
        {
            ApplicationDbContext.Categories.Add(new Category
            {
                Name = "Fruits",
                Products = new List<Product> {
                    new Product { Name = "Banana", Price = new Decimal(10.05), AmountAvailable = 2, CategoryId = 1 },
                    new Product { Name = "Apple", Price = new Decimal(15.50), AmountAvailable = 5, CategoryId = 1 }
                   }
            });
            ApplicationDbContext.Categories.Add(new Category
            {
                Name = "Vegetables",
                Products = new List<Product> {
                    new Product { Name = "Onion", Price = new Decimal(2.45), AmountAvailable = 20, CategoryId = 2 },
                    new Product { Name = "Tomato", Price = new Decimal(3.50), AmountAvailable = 3, CategoryId = 2 }
                   }
            });
            ApplicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            ApplicationDbContext.Database.EnsureDeleted();
            ApplicationDbContext.Dispose();
        }
    }
}
