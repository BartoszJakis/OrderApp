using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;

namespace OrderConsoleApp.Repostiory
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrderById(Guid id);
        List<Order> GetOrders();
        void UpdateOrder(Guid id, OrderStatus status);
    }
}