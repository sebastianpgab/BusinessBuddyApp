using BusinessBuddyApp.Entities;
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
            var products = await _dbContext.Products.Include(p => p.Clothe).Include(w => w.Mug).Include(c => c.Other).ToListAsync();
            if (products.Any())
            {
                return products;
            }
            throw new ArgumentNullException("No products found");
        }

        public async Task<Product> Get(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.Clothe)
                .Include(p => p.Mug)
                .Include(p => p.Other)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is not null)
            {
                return product;
            }
            throw new ArgumentNullException("Product not found");

        }

        //Think about the strategy pattern
        public async Task<Product> Update(Product updatedProduct, int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.Mug)
                .Include(p => p.Clothe)
                .Include(p => p.Other)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new Exception($"Product with ID {id} not found.");

            product.ProductType = updatedProduct.ProductType ?? product.ProductType;
            product.Price = updatedProduct.Price ?? product.Price;
            product.Quantity = updatedProduct.Quantity ?? product.Quantity;
            product.StockQuantity = updatedProduct.StockQuantity ?? product.StockQuantity;

            switch (product.ProductType)
            {
                case "Mug":
                    if (product.Mug is not null)
                    {
                        product.Mug.Material = updatedProduct.Mug.Material ?? product.Mug.Material;
                        product.Mug.Capacity = updatedProduct.Mug.Capacity ?? product.Mug.Capacity;
                        product.Mug.IsMicrowaveSafe = updatedProduct.Mug.IsMicrowaveSafe ?? product.Mug.IsMicrowaveSafe;
                        product.Mug.IsDishwasherSafe = updatedProduct.Mug.IsDishwasherSafe ?? product.Mug.IsDishwasherSafe;
                    }
                    break;

                case "Clothe":
                    if (product.Clothe is not null)
                    {
                        product.Clothe.Size = updatedProduct.Clothe.Size ?? product.Clothe.Size;
                        product.Clothe.Gender = updatedProduct.Clothe.Gender ?? product.Clothe.Gender;
                        product.Clothe.Brand = updatedProduct.Clothe.Brand ?? product.Clothe.Brand;
                        product.Clothe.Style = updatedProduct.Clothe.Style ?? product.Clothe.Style;
                    }
                    break;

                case "Other":
                    if (product.Other is not null)
                    {
                        product.Other.Description = updatedProduct.Other.Description ?? product.Other.Description;
                    }
                    break;

                default:
                    throw new Exception($"Unknown product type: {updatedProduct.ProductType}.");
            }

            await _dbContext.SaveChangesAsync();
            return updatedProduct;
        }

        public bool Create(Product product)
        {

            if (product is not null)
            {
                _dbContext.Add(product);
                _dbContext.SaveChanges();
                return true;
            }

            throw new ArgumentNullException("Product" + nameof(product) + "is null");
 
        }



    }
}
