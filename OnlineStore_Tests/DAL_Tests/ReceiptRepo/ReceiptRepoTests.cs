using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;

namespace OnlineStore_Tests.DAL_Tests.ReceiptRepo
{
    [TestFixture]
    public class ReceiptRepoTests
    {
        private static ReceiptRepository _receiptRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            Receipt nullReceipt = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _receiptRepo.AddAsync(nullReceipt));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException),
                "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var receipt = new Receipt();
            var expectedCount = 3;

            // Act
            await _receiptRepo.AddAsync(new Receipt
            {
                User = new User
                {
                    UserName = "tUser3"
                },
                UserId = 3,
                Date = DateTime.Now,
                City = "",
                Address = "",
                Products = new List<Product>
                {
                    new Product {Name = "Onion", Price = new decimal(2.45), AmountAvailable = 20, CategoryId = 2},
                    new Product {Name = "Tomato", Price = new decimal(3.50), AmountAvailable = 3, CategoryId = 2}
                }
            });
            var actualCount = fixture.ApplicationDbContext.Receipts.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Receipt wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _receiptRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Receipt was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var existingId = 1;
            var expectedCount = 1;

            // Act
            await _receiptRepo.DeleteAsync(existingId);
            var actualCount = fixture.ApplicationDbContext.Receipts.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Receipt wasn't deleted.");
        }

        [Test]
        public async Task GetAllAsync()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var expectedCount = 2;

            // Act
            var actualCount = (await _receiptRepo.GetAllAsync()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Receipt count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _receiptRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Receipt wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var existingId = 1;
            var expectedReceiptUserName = "tUser1";

            // Act
            var actualReceiptUserName = (await _receiptRepo.GetAsync(existingId)).User.UserName;

            // Assert
            Assert.That(actualReceiptUserName, Is.EqualTo(expectedReceiptUserName), "Receipts was not equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange 
            Receipt nullReceipt = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _receiptRepo.UpdateAsync(nullReceipt));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Receipt was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new ReceiptSeedDataFixture();
            _receiptRepo = new ReceiptRepository(fixture.ApplicationDbContext);

            // Arrange
            var receipt = await _receiptRepo.GetAsync(1);
            var expectedProductCount = 3;
            receipt.Products.Add(new Product());

            // Act
            await _receiptRepo.UpdateAsync(receipt);
            var actualProductCount = (await _receiptRepo.GetAsync(1)).Products.Count();

            // Assert
            Assert.That(actualProductCount, Is.EqualTo(expectedProductCount), "Receipt's product count aren't equal.");
        }
    }
}