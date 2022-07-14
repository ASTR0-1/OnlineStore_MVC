﻿using Microsoft.EntityFrameworkCore;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore_DAL.Repositories
{
    public class ShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        ApplicationDbContext _context;

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

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts.Include(sc => sc.Products);
        }

        public async Task<ShoppingCart> GetAsync(int id)
        {
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.Id == id);

            if (shoppingCart != null)
                return shoppingCart;
            else
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
