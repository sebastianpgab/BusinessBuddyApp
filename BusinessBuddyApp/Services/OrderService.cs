﻿using BusinessBuddyApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IOrderService
    {
        public Task<Order> Get(int id);
        public bool Create(int clientId);
        public Task<IEnumerable<Order>> GetAll();

    }
    public class OrderService : IOrderService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public OrderService(BusinessBudyDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<Order> Get(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (order is not null)
            {
                return order;
            }

            throw new ArgumentNullException("Order not found");
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            if (orders.Any())
            {
                return orders;
            }
            throw new ArgumentNullException("Orders not found");
        }

        public bool Create(int clientId)
        {
            var order = new Order();
            {
                order.ClientId = clientId;
                order.OrderDetails = new List<OrderDetail>();
            }
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return true;
        }

    }
}