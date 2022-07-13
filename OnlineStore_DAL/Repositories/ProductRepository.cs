using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class ProductRepository : IGenericRepository<Product>
    {
        ApplicationDbContext _context;

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

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(p => p.WishLists)
                .Include(p => p.Receipts).Include(p => p.ShoppingCarts);
        }

        public async Task<Product> GetAsync(int id)
        {
            var receipt = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (receipt != null)
                return receipt;
            else
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
