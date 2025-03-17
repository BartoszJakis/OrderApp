using OrderConsoleApp.Enum;
using OrderConsoleApp.Interaction;
using OrderConsoleApp.Model;
using OrderConsoleApp.Repostiory;
using OrderConsoleApp.Services;


namespace OrderApp.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CreateOrderTest()
        {

            var orderRepository= new OrderRepository();
            var orderService = new OrderService(orderRepository); 
            var orderConsoleUI = new OrderConsoleUI(orderService);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductName = "Kostka Rubika",
                Payment = new Payment { Price = 12, PaymentType = PaymentType.Card },
                Client = new Client { ClientType = ClientType.Person },
                Address = "Testowa 10",
                OrderStatus = OrderStatus.New
            };

            orderService.CreateOrder(order);
            var orders = orderService.GetOrders();

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual("Kostka Rubika", orders[0].ProductName);
            Assert.AreEqual(12, orders[0].Payment.Price);
            Assert.AreEqual(PaymentType.Card, orders[0].Payment.PaymentType);
            Assert.AreEqual(ClientType.Person, orders[0].Client.ClientType);
            Assert.AreEqual("Testowa 10", orders[0].Address);
            Assert.AreEqual(OrderStatus.New, orders[0].OrderStatus);
        }

        [TestMethod]
        public void ToWarehouseOrderTest()
        {

            var orderRepository = new OrderRepository();
            var orderService = new OrderService(orderRepository);
            var orderConsoleUI = new OrderConsoleUI(orderService);
            

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductName = "Kostka Rubika",
                Payment = new Payment { Price = 12, PaymentType = PaymentType.Card },
                Client = new Client { ClientType = ClientType.Person },
                Address = "Testowa 10",
                OrderStatus = OrderStatus.New
            };

            orderService.CreateOrder(order);
            orderService.MoveToWarehouse(order.Id);
            var updatedOrder = orderService.GetOrders().Find(o => o.Id == order.Id);

            Assert.IsNotNull(updatedOrder);
            Assert.AreEqual(OrderStatus.InWarehouse, updatedOrder.OrderStatus);
        }


        [TestMethod]
        public void MoveToShipmentTest_ValidId()
        {
          
            var orderRepository = new OrderRepository();
            var orderService = new OrderService(orderRepository);
            var orderConsoleUI = new OrderConsoleUI(orderService);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductName = "Kostka Rubika",
                Payment = new Payment { Price = 12, PaymentType = PaymentType.Card },
                Client = new Client { ClientType = ClientType.Person },
                Address = "Testowa 10",
                OrderStatus = OrderStatus.InWarehouse
            };

            orderService.CreateOrder(order);
            orderService.MoveToWarehouse(order.Id); 

            orderService.MoveToShipment(order.Id);

            var updatedOrder = orderService.GetOrders().Find(o => o.Id == order.Id);
            Assert.IsNotNull(updatedOrder);
            Assert.AreEqual(OrderStatus.InShipment, updatedOrder.OrderStatus);
        }


    }
    
}
