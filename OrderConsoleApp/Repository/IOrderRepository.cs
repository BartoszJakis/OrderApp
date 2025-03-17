using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;

namespace OrderConsoleApp.Repostiory
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(Guid id);
        Task UpdateOrder(Guid id, OrderStatus status);
    }
}