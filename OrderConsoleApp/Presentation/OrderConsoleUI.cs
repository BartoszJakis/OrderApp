using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using OrderConsoleApp.Services;
using System;

namespace OrderConsoleApp.Interaction
{
    public class OrderConsoleUI : IOrderConsoleUI
    {
        private readonly IOrderService _orderService;

        public OrderConsoleUI(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private void CreateOrder()
        {
            try
            {
                Console.Write("\nEnter Product Name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter Order Price (for example: 3,12): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid Price. Please use a comma (,) as the decimal separator.");
                    return;
                }

                Console.Write("Enter Client Type (1 = Company, 2 = Person): ");
                string clientTypeInput = Console.ReadLine();
                ClientType clientType = clientTypeInput == "1" ? ClientType.Company : ClientType.Person;

                Console.Write("Enter Delivery Address: ");
                string address = Console.ReadLine();

                Console.Write("Payment Type (1 = Card, 2 = Cash on Delivery): ");
                string paymentTypeInput = Console.ReadLine();
                PaymentType paymentType = paymentTypeInput == "1" ? PaymentType.Card : PaymentType.CashWhenDelivered;

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    ProductName = productName,
                    Payment = new Payment { Price = (float)price, PaymentType = paymentType },
                    Client = new Client { ClientType = clientType },
                    Address = address,
                    OrderStatus = OrderStatus.New
                };

                _orderService.CreateOrder(order);
                Console.WriteLine($"Order {order.Id} has been successfully created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void MoveToWarehouse()
        {
            try
            {
                Console.Write("\nEnter Order ID: ");
                if (!Guid.TryParse(Console.ReadLine(), out Guid orderId))
                {
                    Console.WriteLine("Invalid Order ID.");
                    return;
                }

                _orderService.MoveToWarehouse(orderId);
                Console.WriteLine($"Order {orderId} moved to warehouse.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void MoveToShipment()
        {
            try
            {
                Console.Write("\nEnter Order ID: ");
                if (!Guid.TryParse(Console.ReadLine(), out Guid orderId))
                {
                    Console.WriteLine("Invalid Order ID.");
                    return;
                }

              _orderService.MoveToShipment(orderId);
                Console.WriteLine($"Order {orderId} is being shipped...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DisplayOrders()
        {
            try
            {
                var orders = _orderService.GetOrders();
                if (orders.Count == 0)
                {
                    Console.WriteLine("No orders available.");
                    return;
                }

                Console.WriteLine("\n--- Orders List ---");
                foreach (var order in orders)
                {
                    DisplayProduct(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Order Management System ---");
                Console.WriteLine("1. Create Order");
                Console.WriteLine("2. Move Order to Warehouse");
                Console.WriteLine("3. Move Order to Shipment");
                Console.WriteLine("4. Display Orders");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        MoveToWarehouse();
                        break;
                    case "3":
                        MoveToShipment();
                        break;
                    case "4":
                        DisplayOrders();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }


        private void DisplayProduct(Order order)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("|             ORDER DETAILS                  |");
            Console.WriteLine("==============================================");
            Console.WriteLine($"|  ID:          {order.Id}");
            Console.WriteLine($"|  Product:     {order.ProductName}");
            Console.WriteLine($"|  Price:       {order.Payment.Price:C}");
            Console.WriteLine($"|  Client Type: {order.Client.ClientType}");
            Console.WriteLine($"|  Address:     {order.Address}");
            Console.WriteLine($"|  Status:      {order.OrderStatus}");
            Console.WriteLine("==============================================\n");
        }


    }
}