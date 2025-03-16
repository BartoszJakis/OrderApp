using OrderConsoleApp.Model;

namespace OrderConsoleApp.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        List<Order> GetOrders();
        void MoveToShipment(Guid orderId);
        void MoveToWarehouse(Guid orderId);
    }
}