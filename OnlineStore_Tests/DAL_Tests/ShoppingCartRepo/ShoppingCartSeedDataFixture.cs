using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;

namespace OnlineStore_Tests.DAL_Tests.ShoppingCartRepo
{
    public class ShoppingCartSeedDataFixture : IDisposable
    {
        public ShoppingCartSeedDataFixture()
        {
            ApplicationDbContext.ShoppingCarts.Add(new ShoppingCart
            {
                User = new User
                {
                    UserName = "tUser1"
                },
                UserId = 1,
                Products = new List<Product>
                {
                    new Product {Name = "Onion", Price = new decimal(2.45), AmountAvailable = 20, CategoryId = 2},
                    new Product {Name = "Tomato", Price = new decimal(3.50), AmountAvailable = 3, CategoryId = 2}
                }
            });
            ApplicationDbContext.ShoppingCarts.Add(new ShoppingCart
            {
                User = new User
                {
                    UserName = "tUser2"
                },
                UserId = 2,
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