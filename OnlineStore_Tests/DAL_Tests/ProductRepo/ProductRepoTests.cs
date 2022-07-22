using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Tests.DAL_Tests.ProductRepo
{
    [TestFixture]
    public class ProductRepoTests
    {
        private static ProductRepository _productRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            Product nullProduct = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _productRepo.AddAsync(nullProduct));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            Product product = new Product();
            int expectedCount = 3;

            // Act
            await _productRepo.AddAsync(new Product { Name = "Carrot", Price = new Decimal(5.45), AmountAvailable = 20, CategoryId = 2 });
            var actualCount = fixture.ApplicationDbContext.Products.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Product wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _productRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Product was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            int expectedCount = 1;

            // Act
            await _productRepo.DeleteAsync(existingId);
            int actualCount = fixture.ApplicationDbContext.Products.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Product wasn't deleted.");
        }

        [Test]
        public async Task GetAllAsync()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            int expectedCount = 2;

            // Act
            int actualCount = (await _productRepo.GetAllAsync()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Product count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _productRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Products wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            string expectedProductName = "Onion";

            // Act
            string actualProductName = (await _productRepo.GetAsync(existingId)).Name;

            // Assert
            Assert.That(actualProductName, Is.EqualTo(expectedProductName), "Products was not equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange 
            Product nullProduct = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _productRepo.UpdateAsync(nullProduct));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Product was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new ProductSeedDataFixture();
            _productRepo = new ProductRepository(fixture.ApplicationDbContext);

            // Arrange
            Product product = await _productRepo.GetAsync(1);
            string expectedName = "Opnion";
            product.Name = expectedName;

            // Act
            await _productRepo.UpdateAsync(product);
            string actualName = (await _productRepo.GetAsync(1)).Name;

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName), "Product's names aren't equal.");
        }
    }
}
