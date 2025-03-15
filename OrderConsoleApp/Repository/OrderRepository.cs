using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Repostiory
{
    public class OrderRepository
    {
        private List<Order> _orders = new List<Order>(); 

        public void AddOrder (Order order)
        {
            _orders.Add(order);

        }

        public List<Order> GetOrders()
        { 
            return _orders; 
        }

        public Order GetOrderById (Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Guid id, Order order, OrderStatus status)
        {
            var updatedOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (order != null)
            {
                updatedOrder.OrderStatus = status;
            }
        }

    }
}
