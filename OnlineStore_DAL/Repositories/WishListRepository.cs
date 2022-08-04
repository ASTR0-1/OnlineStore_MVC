using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class WishListRepository : IGenericRepository<WishList>
    {
        private readonly ApplicationDbContext _context;

        public WishListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WishList entity)
        {
            if (entity != null)
                _context.WishLists.Add(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var wishListToDelete = await _context.WishLists.FirstOrDefaultAsync(r => r.Id == id);

            if (wishListToDelete != null)
                _context.WishLists.Remove(wishListToDelete);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WishList>> GetAllAsync()
        {
            return await _context.WishLists
                .Include(w => w.Products)
                .ThenInclude(p => p.Image)
                .Include(w => w.User)
                .ToListAsync();
        }

        public async Task<WishList> GetAsync(int id)
        {
            var wishlist = await _context.WishLists
                .Include(w => w.Products)
                .ThenInclude(p => p.Image)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (wishlist != null)
                return wishlist;
            throw new NullReferenceException();
        }

        public async Task UpdateAsync(WishList entity)
        {
            if (entity != null)
                _context.WishLists.Update(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }
    }
}