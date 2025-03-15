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
        private List<Order> orders = new List<Order>(); 

        public void AddOrder (Order order)
        {
            orders.Add(order);

        }

        public List<Order> GetOrders()
        { 
            return orders; 
        }

        public Order GetOrderById (Guid id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }


    }
}
