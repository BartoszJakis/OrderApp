using OrderConsoleApp.Model;

namespace OrderConsoleApp.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task MoveToShipment(Guid orderId);
        Task MoveToWarehouse(Guid orderId);
    }
}