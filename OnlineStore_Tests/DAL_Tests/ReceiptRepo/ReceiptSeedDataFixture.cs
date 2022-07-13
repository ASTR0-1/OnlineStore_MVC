using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;

namespace OnlineStore_Tests.DAL_Tests.ReceiptRepo
{
    public class ReceiptSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public ReceiptSeedDataFixture()
        {
            ApplicationDbContext.Receipts.Add(new Receipt
            {
                User = new User
                {
                    UserName = "tUser1",
                },
                UserId = 1,
                Date = DateTime.Now,
                City = "",
                Address = "",
                Products = new List<Product>
                {
                    new Product { Name = "Onion", Price = new Decimal(2.45), AmountAvailable = 20, CategoryId = 2 },
                    new Product { Name = "Tomato", Price = new Decimal(3.50), AmountAvailable = 3, CategoryId = 2 }
                }
            });
            ApplicationDbContext.Receipts.Add(new Receipt
            {
                User = new User
                {
                    UserName = "tUser2",
                },
                UserId = 2,
                Date = DateTime.Now,
                City = "",
                Address = "",
                Products = new List<Product>
                {
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
