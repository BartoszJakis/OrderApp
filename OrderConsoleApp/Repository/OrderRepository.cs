using Microsoft.EntityFrameworkCore;
using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Repostiory
{
    public class OrderRepository : IOrderRepository
    {
       private readonly OrderAppDbContext _dbContext;

        public OrderRepository(OrderAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrder(Guid id, OrderStatus status)
        {
            var updatedOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id ==id );
            if (updatedOrder != null)
            {
                updatedOrder.OrderStatus = status;
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
