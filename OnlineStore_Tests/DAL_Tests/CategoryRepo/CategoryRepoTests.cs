using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Tests.DAL_Tests.CategoryRepo
{
    [TestFixture]
    public class CategoryRepoTests
    {
        private static CategoryRepository _categoryRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            Category nullCategory = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _categoryRepo.AddAsync(nullCategory));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            Category category = new Category();
            int expectedCount = 3;

            // Act
            await _categoryRepo.AddAsync(new Category
            {
                Name = "Bread",
                Products = new List<Product> {
                        new Product { Name = "Black bread", Price = new Decimal(12.25), AmountAvailable = 2, CategoryId = 3 },
                       }
            });
            var actualCount = fixture.ApplicationDbContext.Categories.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Category wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _categoryRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Category was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            int expectedCount = 1;

            // Act
            await _categoryRepo.DeleteAsync(existingId);
            int actualCount = fixture.ApplicationDbContext.Categories.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Category wasn't deleted.");
        }

        [Test]
        public void GetAll()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            int expectedCount = 2;

            // Act
            int actualCount = _categoryRepo.GetAll().Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Categories count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _categoryRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Categories wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            string expectedCategoryName = "Fruits";

            // Act
            string actualCategoryName = (await _categoryRepo.GetAsync(existingId)).Name;

            // Assert
            Assert.That(actualCategoryName, Is.EqualTo(expectedCategoryName), "Categories was not equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange 
            Category nullCategory = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _categoryRepo.UpdateAsync(nullCategory));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Category was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new CategorySeedDataFixture();
            _categoryRepo = new CategoryRepository(fixture.ApplicationDbContext);

            // Arrange
            Category category = await _categoryRepo.GetAsync(1);
            string expectedCategoryName = "ChangedFruits";
            category.Name = expectedCategoryName;

            // Act
            await _categoryRepo.UpdateAsync(category);
            string actualCategoryName = (await _categoryRepo.GetAsync(1)).Name;

            // Assert
            Assert.That(actualCategoryName, Is.EqualTo(expectedCategoryName), "Category names aren't equal.");
        }
    }
}