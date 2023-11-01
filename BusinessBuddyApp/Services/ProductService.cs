using BusinessBuddyApp.Entities;
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
            var products = await _dbContext.Products.Include(p => p.Clothe).Include(w => w.Mug).Include(c => c.Other).ToListAsync();
            if (products.Any())
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

        public async Task<Product> Update(Product updatedProduct, int id)
        {       
            switch (updatedProduct.ProductType)
            {
                case "Mug":

                    var mugProduct = await _dbContext.Mugs.FirstOrDefaultAsync(p => p.Id == id);

                    if(mugProduct is not null)
                    {
                        if (updatedProduct.Mug.Material is not null) { mugProduct.Material = updatedProduct.Mug.Material; }
                        if (updatedProduct.Mug.Capacity is not null) { mugProduct.Capacity = updatedProduct.Mug.Capacity; }
                        if (updatedProduct.Mug.IsMicrowaveSafe is not null) { mugProduct.IsMicrowaveSafe = updatedProduct.Mug.IsMicrowaveSafe;}
                        if (updatedProduct.Mug.IsDishwasherSafe is not null) { mugProduct.IsDishwasherSafe = updatedProduct.Mug.IsDishwasherSafe;}
                    }
                    break;

                case "Clothe":

                    var clotheProduct = await _dbContext.Clothes.FirstOrDefaultAsync(p => p.Id == id);

                    if (clotheProduct is not null)
                    {
                        if (updatedProduct.Clothe.Size is not null) { clotheProduct.Size = updatedProduct.Clothe.Size; }
                        if (updatedProduct.Clothe.Gender is not null) {clotheProduct.Gender = updatedProduct.Clothe.Gender; }
                        if (updatedProduct.Clothe.Brand is not null) {clotheProduct.Brand = updatedProduct.Clothe.Brand; }
                        if (updatedProduct.Clothe.Style is not null) { clotheProduct.Style = updatedProduct.Clothe.Style; }
                    }
                    break;

                case "Orther":

                    var otherProduct = await _dbContext.Others.FirstOrDefaultAsync(p => p.Id == id);

                    if(otherProduct is not null)
                    {
                        if (updatedProduct.Other.Description is not null) { otherProduct.Description = updatedProduct.Other.Description;}
                    }
                    break;

                default:
                    throw new Exception($"Unknown product type: {updatedProduct.ProductType}.");
                                           
            }

            await _dbContext.SaveChangesAsync();
            return updatedProduct;
            
        }


    }
}
