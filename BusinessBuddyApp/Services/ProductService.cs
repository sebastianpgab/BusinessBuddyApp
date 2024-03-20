using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> Get(int id);
        public Task<Product> Update(Product updatedProduct, int id);
        public bool Create(Product product);

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
            var products = await _dbContext.Products.ToListAsync();
            if (products.Any())
            {
                return products;
            }
            throw new NotFoundException("No products found");
        }

        public async Task<Product> Get(int id)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null)
            {
                return product;
            }
            throw new NotFoundException("Product not found");

        }

        //Think about the strategy pattern
        public async Task<Product> Update(Product updatedProduct, int id)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new Exception($"Product with ID {id} not found.");
            if (product.Description.Length > 100) throw new ArgumentOutOfRangeException("Description", "The product description cannot exceed 100 characters.");

            product.ProductType = updatedProduct.ProductType != product.ProductType ? updatedProduct.ProductType : product.ProductType;
            product.Description = updatedProduct.Description != product.Description ? updatedProduct.Description : product.Description;
            product.Price = updatedProduct.Price != product.Price ? product.Price : updatedProduct.Price;
            product.StockQuantity = updatedProduct.StockQuantity != product.StockQuantity ? updatedProduct.StockQuantity : product.StockQuantity;

            await _dbContext.SaveChangesAsync();
            return updatedProduct;
        }

        public bool Create(Product product)
        {
            if (product is not null)
            {
                if(product.Description.Length > 100)
                {
                    _dbContext.Add(product);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Description", "The product description cannot exceed 100 characters.");
                }
            }

            throw new ArgumentNullException("Product" + nameof(product) + "is null");
        }
    }
}
