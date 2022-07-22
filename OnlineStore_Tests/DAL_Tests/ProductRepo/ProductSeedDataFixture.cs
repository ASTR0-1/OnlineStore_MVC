using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;

namespace OnlineStore_Tests.DAL_Tests.ProductRepo
{
    public class ProductSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public ProductSeedDataFixture()
        {
            ApplicationDbContext.Products.Add(new Product
            {
                Name = "Onion",
                Price = new Decimal(2.45),
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
                Price = new Decimal(3.50),
                AmountAvailable = 3,
                Category = new Category(),
                WishLists = new List<WishList>(),
                Receipts = new List<Receipt>(),
                ShoppingCarts = new List<ShoppingCart>()
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