using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;
using System;

namespace OnlineStore_Tests.DAL_Tests.ProductRepo
{
    public class ProductSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public ProductSeedDataFixture()
        {
            ApplicationDbContext.Products.Add(new Product { Name = "Onion", Price = new Decimal(2.45), AmountAvailable = 20, CategoryId = 2 });
            ApplicationDbContext.Products.Add(new Product { Name = "Tomato", Price = new Decimal(3.50), AmountAvailable = 3, CategoryId = 2 });

            ApplicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            ApplicationDbContext.Database.EnsureDeleted();
            ApplicationDbContext.Dispose();
        }
    }
}