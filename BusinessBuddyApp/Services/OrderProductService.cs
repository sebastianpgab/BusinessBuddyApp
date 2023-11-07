using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IOrderProductService
    {
        public Task<OrderProduct> Get(int id);
        public Task<IEnumerable<OrderProduct>> GetAll(int orderDetailId);
        public bool Update(OrderProduct orderProduct, int id);
        public bool Delete(int orderProductId);
        public OrderProduct Create(OrderProduct orderProduct);

    }
    public class OrderProductService : IOrderProductService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public OrderProductService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OrderProduct> Get(int id)
        {
            var orderProduct = await _dbContext.OrderProducts.FirstOrDefaultAsync(p => p.Id == id);
            if (orderProduct is not null)
            {
                return orderProduct;
            }
            throw new ArgumentNullException("Order Product not found");
        }

        public async Task<IEnumerable<OrderProduct>> GetAll(int orderDetailId)
        {
            var orderProducts = await _dbContext.OrderProducts.Where(p => p.OrderDetailId == orderDetailId).ToListAsync();

            if(orderProducts.Any())
            {
                return orderProducts;
            }
            throw new ArgumentNullException("Order Products not found");
        }

        public bool Update(OrderProduct orderProduct, int id) 
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct));
            }
            var existingOrderProduct = _dbContext.OrderProducts.FirstOrDefault(op => op.Id == id);

            if (existingOrderProduct == null)
            {
                throw new ArgumentNullException("Order product not found.");
            }

            existingOrderProduct.TotalAmount = orderProduct.TotalAmount;
            existingOrderProduct.Quantity = orderProduct.Quantity;
            existingOrderProduct.ProductId = orderProduct.ProductId;
            existingOrderProduct.OrderDetailId = orderProduct.OrderDetailId;

            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int orderProductId)
        {
            var orderProduct = _dbContext.OrderProducts.Find(orderProductId);

            if (orderProduct == null)
            {
                throw new InvalidOperationException("Order product not found.");
            }

            _dbContext.OrderProducts.Remove(orderProduct);

            _dbContext.SaveChanges();
            return true;
        }

        public OrderProduct Create(OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct));
            }
            _dbContext.OrderProducts.Add(orderProduct);

            _dbContext.SaveChanges();

            return orderProduct;
        }
    }
}
