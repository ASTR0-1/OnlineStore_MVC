using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;

namespace OnlineStore_Tests.DAL_Tests.ImageRepo
{
    public class ImageSeedDataFixture : IDisposable
    {
        public ImageSeedDataFixture()
        {
            ApplicationDbContext.Images.Add(new Image
            {
                Name = "PotatoImg",
                Product = new Product
                {
                    Name = "Potato",
                    Price = new decimal(1.50),
                    AmountAvailable = 2,
                    CategoryId = 1
                },
                ProductId = 1
            });
            ApplicationDbContext.Images.Add(new Image
            {
                Name = "TomatoImg",
                Product = new Product
                {
                    Name = "Tomato",
                    Price = new decimal(2.50),
                    AmountAvailable = 5,
                    CategoryId = 1
                },
                ProductId = 2
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