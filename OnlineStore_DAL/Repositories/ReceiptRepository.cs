using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class ReceiptRepository : IGenericRepository<Receipt>
    {
        private readonly ApplicationDbContext _context;

        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Receipt entity)
        {
            if (entity != null)
                _context.Receipts.Add(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var receiptToDelete = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);

            if (receiptToDelete != null)
                _context.Receipts.Remove(receiptToDelete);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await _context.Receipts
                .Include(r => r.User)
                .Include(r => r.Products)
                .ThenInclude(p => p.Image)
                .ToListAsync();
        }

        public async Task<Receipt> GetAsync(int id)
        {
            var receipt = await _context.Receipts
                .Include(r => r.User)
                .Include(r => r.Products)
                .ThenInclude(p => p.Image)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receipt != null)
                return receipt;
            throw new NullReferenceException();
        }

        public async Task UpdateAsync(Receipt entity)
        {
            if (entity != null)
                _context.Receipts.Update(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }
    }
}