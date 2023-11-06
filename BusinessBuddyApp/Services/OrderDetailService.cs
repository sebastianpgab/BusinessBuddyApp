using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IOrderDetailService
    {
        public Task<OrderDetail> Get(int id);
        public Task<IEnumerable<OrderDetail>> GetAll();
        public bool Create(int orderId);

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
            throw new ArgumentNullException("OrderDetail not found");
        }

        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            var orderDetails = await _dbContext.OrderDetails.ToListAsync();
            if (orderDetails.Any())
            {
                return orderDetails;
            }
            throw new ArgumentNullException("OrderDetails not found");
        }

        public bool Create(int orderId)
        {
            var orderDetails = new OrderDetail()
            {
                OrderId = orderId
            };
            _dbContext.OrderDetails.Add(orderDetails);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
