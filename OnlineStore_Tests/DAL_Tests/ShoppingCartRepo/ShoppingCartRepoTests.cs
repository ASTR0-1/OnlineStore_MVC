using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore_Tests.DAL_Tests.ShoppingCartRepo
{
    [TestFixture]
    public class ShoppingCartRepoTests
    {
        private static ShoppingCartRepository _shoppingCartRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            ShoppingCart nullShoppingCart = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _shoppingCartRepo.AddAsync(nullShoppingCart));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            ShoppingCart shoppingCart = new ShoppingCart();
            int expectedCount = 3;

            // Act
            await _shoppingCartRepo.AddAsync(new ShoppingCart
            {
                User = new User
                {
                    UserName = "tUser3",
                },
                UserId = 3,
                Products = new List<Product>
                {
                    new Product { Name = "Onion", Price = new Decimal(2.45), AmountAvailable = 20, CategoryId = 2 },
                    new Product { Name = "Tomato", Price = new Decimal(3.50), AmountAvailable = 3, CategoryId = 2 }
                }
            });
            var actualCount = fixture.ApplicationDbContext.ShoppingCarts.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "ShoppingCart wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _shoppingCartRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "ShoppingCart was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            int expectedCount = 1;

            // Act
            await _shoppingCartRepo.DeleteAsync(existingId);
            int actualCount = fixture.ApplicationDbContext.ShoppingCarts.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "ShoppingCart wasn't deleted.");
        }

        [Test]
        public async Task GetAllAsync()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            int expectedCount = 2;

            // Act
            int actualCount = (await _shoppingCartRepo.GetAllAsync()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "ShoppingCart count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _shoppingCartRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "ShoppingCart wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            string expectedShopppingCartUserName = "tUser1";

            // Act
            string actualShoppingCartUserName = (await _shoppingCartRepo.GetAsync(existingId)).User.UserName;

            // Assert
            Assert.That(actualShoppingCartUserName, Is.EqualTo(expectedShopppingCartUserName), "ShoppingCarts was not equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange 
            ShoppingCart nullShoppingCart = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _shoppingCartRepo.UpdateAsync(nullShoppingCart));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "ShoppingCart was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new ShoppingCartSeedDataFixture();
            _shoppingCartRepo = new ShoppingCartRepository(fixture.ApplicationDbContext);

            // Arrange
            ShoppingCart shoppingCart = await _shoppingCartRepo.GetAsync(1);
            int expectedProductCount = 3;
            shoppingCart.Products.Add(new Product());

            // Act
            await _shoppingCartRepo.UpdateAsync(shoppingCart);
            int actualProductCount = ((await _shoppingCartRepo.GetAsync(1)).Products).Count();

            // Assert
            Assert.That(actualProductCount, Is.EqualTo(expectedProductCount), "ShoppingCart's product count aren't equal.");
        }
    }
}
