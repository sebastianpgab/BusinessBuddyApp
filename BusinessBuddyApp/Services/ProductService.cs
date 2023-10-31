﻿using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> Get(int id);
    }
    public class ProductService : IProductService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public ProductService(BusinessBudyDbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = _dbContext.Products.Include(p => p.Clothe).Include(w => w.Mug).Include(c => c.Other);
            if(products.Any())
            {
                return products;
            }
            throw new ArgumentNullException("No products found");
        }

        public async Task<Product> Get(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product is not null)
            {
                return product;
            }
            throw new ArgumentNullException("Product not found");
        }


    }
}
