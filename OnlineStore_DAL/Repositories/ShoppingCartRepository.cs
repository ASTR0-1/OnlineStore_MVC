using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class ShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShoppingCart entity)
        {
            if (entity != null)
                _context.ShoppingCarts.Add(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shoppingCartToDelete = await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.Id == id);

            if (shoppingCartToDelete != null)
                _context.ShoppingCarts.Remove(shoppingCartToDelete);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await _context.ShoppingCarts
                .Include(sc => sc.Products)
                .ThenInclude(p => p.Image)
                .Include(sc => sc.User)
                .ToListAsync();
        }

        public async Task<ShoppingCart> GetAsync(int id)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.Products)
                .ThenInclude(p => p.Image)
                .Include(sc => sc.User)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (shoppingCart != null)
                return shoppingCart;
            throw new NullReferenceException();
        }

        public async Task UpdateAsync(ShoppingCart entity)
        {
            if (entity != null)
                _context.ShoppingCarts.Update(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }
    }
}