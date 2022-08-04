using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;

namespace OnlineStore_DAL.Repositories
{
    public class ImageRepository : IGenericRepository<Image>
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Image entity)
        {
            if (entity != null)
                _context.Images.Add(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var imageToDelete = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (imageToDelete != null)
                _context.Images.Remove(imageToDelete);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Images
                .Include(i => i.Product)
                .ToListAsync();
        }

        public async Task<Image> GetAsync(int id)
        {
            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (image != null)
                return image;
            throw new NullReferenceException();
        }

        public async Task UpdateAsync(Image entity)
        {
            if (entity != null)
                _context.Images.Update(entity);
            else
                throw new NullReferenceException();

            await _context.SaveChangesAsync();
        }
    }
}