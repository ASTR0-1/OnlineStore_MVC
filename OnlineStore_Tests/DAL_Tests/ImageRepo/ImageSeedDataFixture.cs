using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Models;
using System;

namespace OnlineStore_Tests.DAL_Tests.ImageRepo
{
    public class ImageSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("OnlineStore_MVC")
                .Options);

        public ImageSeedDataFixture()
        {
            ApplicationDbContext.Images.Add(new Image
            {
                Name = "PotatoImg",
                Product = new Product
                {
                    Name = "Potato",
                    Price = new Decimal(1.50),
                    AmountAvailable = 2,
                    CategoryId = 1
                },
                ProductId = 1,
            });
            ApplicationDbContext.Images.Add(new Image
            {
                Name = "TomatoImg",
                Product = new Product
                {
                    Name = "Tomato",
                    Price = new Decimal(2.50),
                    AmountAvailable = 5,
                    CategoryId = 1
                },
                ProductId = 2,
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
