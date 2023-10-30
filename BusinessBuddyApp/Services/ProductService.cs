using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
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
            var products =  await _dbContext.Products.Include(p => p.Clothe).Include(w => w.Mug).Include(c => c.Other).ToListAsync();
            if(products.Any())
            {
                return products;
            }
            throw new ArgumentNullException("No products found");
        }


    }
}
