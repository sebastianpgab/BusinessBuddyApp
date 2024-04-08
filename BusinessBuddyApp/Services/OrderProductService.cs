using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
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
        public Task<List<OrderProduct>> Create(List<OrderProduct> orderProducts, int orderDetailId);
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

        public bool Update(OrderProduct updatedOrderProduct, int id)
        {
            if (updatedOrderProduct == null)
            {
                throw new ArgumentNullException(nameof(updatedOrderProduct));
            }

            var existingOrderProduct = _dbContext.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.OrderDetail)
                .SingleOrDefault(op => op.Id == id);

            if (existingOrderProduct == null)
            {
                throw new KeyNotFoundException("Order product not found.");
            }

            if (updatedOrderProduct.Quantity != existingOrderProduct.Quantity)
            {
                if (existingOrderProduct.Product.StockQuantity >= updatedOrderProduct.Quantity)
                {
                    double oldTotalAmount = existingOrderProduct.TotalAmount;                    
                    existingOrderProduct.Quantity = updatedOrderProduct.Quantity;
                    existingOrderProduct.Product.StockQuantity -= existingOrderProduct.Quantity;
                    existingOrderProduct.TotalAmount = updatedOrderProduct.Quantity * existingOrderProduct.Product.Price;

                    UpdateOrderTotalAmount(existingOrderProduct.OrderDetail, existingOrderProduct.TotalAmount - oldTotalAmount);
                }
                else
                {
                    throw new InvalidOperationException("The ordered quantity exceeds available stock.");
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        private void UpdateOrderTotalAmount(OrderDetail orderDetail, double amountDifference)
        {
            if (orderDetail == null)
            {
                throw new ArgumentNullException(nameof(orderDetail));
            }
            orderDetail.FinalAmount += amountDifference;
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

        public async Task<List<OrderProduct>> Create(List<OrderProduct> orderProducts, int orderDetailId)
        {
            if (orderProducts == null || !orderProducts.Any())
            {
                throw new ArgumentNullException(nameof(orderProducts), "OrderProduct cannot be null or empty.");
            }

            var orderDetail = await _dbContext.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail == null)
            {
                throw new NotFoundException("OrderDetail with the given ID was not found.");
            }

            var productIds = orderProducts.Select(op => op.ProductId).Distinct().ToList();
            var products = await _dbContext.Products.Where(product => productIds.Contains(product.Id)).ToListAsync();

            if (products.Count != productIds.Count)
            {
                throw new NotFoundException("One or more products not found.");
            }

            List<OrderProduct> addedOrderProducts = new List<OrderProduct>();

            foreach (var orderProduct in orderProducts)
            {
                var product = products.FirstOrDefault(p => p.Id == orderProduct.ProductId);
                if (product == null)
                {
                    continue;
                }

                if (product.StockQuantity < orderProduct.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for product ID {orderProduct.ProductId}.");
                }

                product.StockQuantity -= orderProduct.Quantity;
                orderProduct.TotalAmount = orderProduct.Quantity * product.Price;
                orderDetail.FinalAmount += orderProduct.TotalAmount;
                orderProduct.OrderDetailId = orderDetailId;

                _dbContext.OrderProducts.Add(orderProduct);
                addedOrderProducts.Add(orderProduct);
            }

            await _dbContext.SaveChangesAsync();

            return addedOrderProducts;
        }

    }
}
