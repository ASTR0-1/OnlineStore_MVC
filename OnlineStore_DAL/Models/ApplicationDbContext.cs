using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore_DAL.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<WishList> WishLists { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}