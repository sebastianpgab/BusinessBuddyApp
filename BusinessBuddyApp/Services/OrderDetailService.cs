using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IOrderDetailService
    {
        public Task<OrderDetail> Get(int id);
        public Task<IEnumerable<OrderDetail>> GetAll();
        public Task<OrderDetail> Create(OrderDetail orderDetail, int orderId);

    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public OrderDetailService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OrderDetail> Get(int id)
        {
            var orderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(p => p.Id == id);
            if(orderDetail is not null) 
            {
                return orderDetail;
            }
            throw new NotFoundException("OrderDetail not found");
        }

        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            var orderDetails = await _dbContext.OrderDetails.ToListAsync();
            if (orderDetails.Any())
            {
                return orderDetails;
            }
            throw new NotFoundException("OrderDetails not found");
        }


        public async Task<OrderDetail> Create(OrderDetail orderDetail, int orderId)
        {
            if(orderDetail != null)
            {
                var order = _dbContext.Orders.FirstOrDefault(p => p.Id == orderId); 
                if(order != null)
                {   
                    orderDetail.OrderId = order.Id;                    
                    _dbContext.OrderDetails.Add(orderDetail);
                    await _dbContext.SaveChangesAsync();

                    order.OrderDetailId = orderDetail.Id;
                    await _dbContext.SaveChangesAsync();
                    return orderDetail;
                }
            }
            throw new InvalidOperationException("Failed to add OrderDetail.");
        }
    }
}
