using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Tests.DAL_Tests.ImageRepo
{
    [TestFixture]
    public class ImageRepoTests
    {
        private static ImageRepository _imageRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            Image nullImage = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _imageRepo.AddAsync(nullImage));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            Image category = new Image();
            int expectedCount = 3;

            // Act
            await _imageRepo.AddAsync(new Image
            {
                Name = "TestImg",
                Product = new Product
                {
                    Name = "Test",
                    Price = new Decimal(1.50),
                    AmountAvailable = 1,
                    CategoryId = 1
                },
                ProductId = 3,
            });
            var actualCount = fixture.ApplicationDbContext.Images.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Image wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _imageRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Image was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            int expectedCount = 1;

            // Act
            await _imageRepo.DeleteAsync(existingId);
            int actualCount = fixture.ApplicationDbContext.Images.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Image wasn't deleted.");
        }

        [Test]
        public async Task GetAllAsync()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            int expectedCount = 2;

            // Act
            int actualCount = (await _imageRepo.GetAllAsync()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Images count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _imageRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Categories wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            string expectedImageName = "PotatoImg";

            // Act
            string actualImageName = (await _imageRepo.GetAsync(existingId)).Name;

            // Assert
            Assert.That(actualImageName, Is.EqualTo(expectedImageName), "Categories was not equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange 
            Image nullImage = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _imageRepo.UpdateAsync(nullImage));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Category was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new ImageSeedDataFixture();
            _imageRepo = new ImageRepository(fixture.ApplicationDbContext);

            // Arrange
            Image image = await _imageRepo.GetAsync(1);
            string expectedImageName = "PotatoImg";
            image.Name = expectedImageName;

            // Act
            await _imageRepo.UpdateAsync(image);
            string actualImageName = (await _imageRepo.GetAsync(1)).Name;

            // Assert
            Assert.That(actualImageName, Is.EqualTo(expectedImageName), "Category names aren't equal.");
        }
    }
}
