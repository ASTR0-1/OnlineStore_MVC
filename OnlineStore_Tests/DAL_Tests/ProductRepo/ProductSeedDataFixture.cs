using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;

namespace OnlineStore_Tests.DAL_Tests.ProductRepo
{
    public class ProductSeedDataFixture : IDisposable
    {
        public ProductSeedDataFixture()
        {
            ApplicationDbContext.Products.Add(new Product
            {
                Name = "Onion",
                Price = new decimal(2.45),
                AmountAvailable = 20,
                Image = new Image(),
                Category = new Category(),
                WishLists = new List<WishList>(),
                Receipts = new List<Receipt>(),
                ShoppingCarts = new List<ShoppingCart>()
            });
            ApplicationDbContext.Products.Add(new Product
            {
                Name = "Tomato",
                Price = new decimal(3.50),
                AmountAvailable = 3,
                Category = new Category(),
                WishLists = new List<WishList>(),
                Receipts = new List<Receipt>(),
                ShoppingCarts = new List<ShoppingCart>()
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