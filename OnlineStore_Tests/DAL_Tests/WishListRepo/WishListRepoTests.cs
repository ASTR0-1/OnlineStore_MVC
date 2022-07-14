using NUnit.Framework;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Tests.DAL_Tests.WishListRepo
{
    [TestFixture]
    public class WishListRepoTests
    {
        private static WishListRepository _wishListRepo;

        [Test]
        public void Add_Null()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            WishList nullWishlist = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _wishListRepo.AddAsync(nullWishlist));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Method doesn't throw null exception.");
        }

        [Test]
        public async Task Add_Not_Null()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            WishList wishList = new WishList();
            int expectedCount = 3;

            // Act
            await _wishListRepo.AddAsync(new WishList
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
            var actualCount = fixture.ApplicationDbContext.WishLists.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "WishList wasn't added.");
        }

        [Test]
        public void Delete_Not_Existing_Id()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _wishListRepo.DeleteAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "WishList was deleted.");
        }

        [Test]
        public async Task Delete_Existing_Id()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            int expectedCount = 1;

            // Act
            await _wishListRepo.DeleteAsync(existingId);
            int actualCount = fixture.ApplicationDbContext.WishLists.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "WishList wasn't deleted.");
        }

        [Test]
        public void GetAll()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            int expectedCount = 2;

            // Act
            int actualCount = _wishListRepo.GetAll().Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "WishLists count wasn't equal to expected.");
        }

        [Test]
        public void GetAsync_Not_Existing_Id()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            int notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _wishListRepo.GetAsync(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "WishList wasn't null.");
        }

        [Test]
        public async Task GetAsync_Existing_Id()
        {
            using var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange
            int existingId = 1;
            string expectedWishListUserName = "tUser1";

            // Act
            string actualWishListUserName = (await _wishListRepo.GetAsync(existingId)).User.UserName;

            // Assert
            Assert.That(actualWishListUserName, Is.EqualTo(expectedWishListUserName), "Wishlists wasn't equal.");
        }

        [Test]
        public void UpdateAsync_Null()
        {
            var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);

            // Arrange 
            WishList nullWishList = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _wishListRepo.UpdateAsync(nullWishList));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "WishList was updated.");
        }

        [Test]
        public async Task UpdateAsync_Not_Null()
        {
            var fixture = new WishListSeedDataFixture();
            _wishListRepo = new WishListRepository(fixture.ApplicationDbContext);
            
            // Arrange
            WishList wishList = await _wishListRepo.GetAsync(1);
            int expectedProductCount = 3;
            wishList.Products.Add(new Product());

            // Act
            await _wishListRepo.UpdateAsync(wishList);
            int actualProductCount = ((await _wishListRepo.GetAsync(1)).Products).Count();

            // Assert
            Assert.That(actualProductCount, Is.EqualTo(expectedProductCount), "WishList's product count aren't equal.");
        }
    }
}
