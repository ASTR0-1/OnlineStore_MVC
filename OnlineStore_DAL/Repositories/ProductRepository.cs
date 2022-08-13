using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class ProductRepository : IGenericRepository<Product>
    {
        private static ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product entity)
        {
            if (entity != null)
                _context.Products.Add(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productToDelete = _context.Products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
                _context.Products.Remove(productToDelete);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.WishLists)
                .ThenInclude(w => w.User)
                .Include(p => p.Receipts)
                .ThenInclude(r => r.User)
                .Include(p => p.ShoppingCarts)
                .ThenInclude(sc => sc.User)
                .Include(p => p.Image)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            var receipt = await _context.Products
                .Include(p => p.WishLists)
                    .ThenInclude(w => w.User)

                .Include(p => p.Receipts)
                    .ThenInclude(r => r.User)

                .Include(p => p.ShoppingCarts)
                    .ThenInclude(sc => sc.User)

                .Include(p => p.Image)
                .Include(p => p.Category)

                .FirstOrDefaultAsync(p => p.Id == id);

            if (receipt != null)
                return receipt;

            throw new NullReferenceException();
        }

        public async Task UpdateAsync(Product entity)
        {
            if (entity != null)
                _context.Products.Update(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }
    }
}