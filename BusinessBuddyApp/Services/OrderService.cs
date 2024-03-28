using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BusinessBuddyApp.Services
{
    public interface IOrderService
    {
        public Task<Order> Get(int clientId, int id);
        public Task<Order> Create(Order order);
        public Task<IEnumerable<Order>> GetAll(int clientId);

    }
    public class OrderService : IOrderService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public OrderService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> Get(int clientId, int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(p => p.Id == id && p.ClientId == clientId);
            if (order is not null)
            {
                return order;
            }

            throw new NotFoundException("Order not found");
        }

        public async Task<IEnumerable<Order>> GetAll(int clientId)
        {
            var orders = await _dbContext.Orders.Where(p => p.ClientId == clientId).ToListAsync();
            if (orders.Any())
            {
                return orders;
            }
            throw new NotFoundException("Orders not found");
        }

        public async Task<Order> Create(Order order)
        {

            if(order != null)
            {
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                return order;
            }
            throw new NotFoundException("Order not found");

        }

    }
}
