using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BusinessBuddyApp.Services
{
    public interface IOrderProductService
    {
        public Task<OrderProduct> Get(int id);
        public Task<IEnumerable<OrderProduct>> GetAll(int orderDetailId);
        public bool Update(OrderProduct orderProduct, int id);
        public bool Delete(int orderProductId);
        public bool Create(OrderProduct orderProduct, int orderDetailId);

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
            throw new InvalidOperationException("Order Product not found");
        }

        public async Task<IEnumerable<OrderProduct>> GetAll(int orderDetailId)
        {
            var orderProducts = await _dbContext.OrderProducts.Where(p => p.OrderDetailId == orderDetailId).ToListAsync();

            if(orderProducts.Any())
            {
                return orderProducts;
            }
            throw new InvalidOperationException("Order Products not found");
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
            var product = _dbContext.Products.First(p => p.Id == existingOrderProduct.ProductId);

            if (orderProduct.Quantity != existingOrderProduct.Quantity)
            {
                if(product.Quantity >= orderProduct.Quantity)
                {
                    existingOrderProduct.Quantity = orderProduct.Quantity;
                    existingOrderProduct.TotalAmount = orderProduct.Quantity * product.Price;
                }
                else
                {
                    throw new InvalidOperationException("The ordered quantity exceeds available stock.");
                }
            }
            
            existingOrderProduct.ProductId = orderProduct.ProductId;

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

        //tu porawic tworzenie tzn. przy tworzeniu zeby od razu obliczało się total amount
        public bool Create(OrderProduct orderProduct, int orderDetailId)
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct), "OrderProduct cannot be null.");
            }
            var orderDetail = _dbContext.OrderDetails.Find(orderDetailId);
            if(orderDetail != null)
            {
                orderProduct.OrderDetailId = orderDetailId;
                _dbContext.OrderProducts.Add(orderProduct);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("OrderDetail with the given ID was not found.");
            }
            return true;
        }
    }
}
