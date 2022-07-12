using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Repositories
{
    public class ImageRepository : IGenericRepository<Image>
    {
        ApplicationDbContext _context;

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

        public IEnumerable<Image> GetAll()
        {
            return _context.Images;
        }

        public async Task<Image> GetAsync(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image != null)
                return image;
            else
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
