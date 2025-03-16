using OrderConsoleApp.Enum;
using OrderConsoleApp.Model;
using OrderConsoleApp.Services;
using System;
using System.Text.RegularExpressions;

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

                string clientTypeInput;
                ClientType clientType;
                do
                {
                    Console.Write("Enter Client Type (1 = Company, 2 = Person): ");
                    clientTypeInput = Console.ReadLine();
                    if(clientTypeInput != "1" && clientTypeInput != "2")
                    {
                        Console.Write("Wrong input! Choose either 1 or 2.");
                        Console.WriteLine();
                    }
                } while (clientTypeInput != "1" && clientTypeInput != "2");

                clientType = clientTypeInput == "1" ? ClientType.Company : ClientType.Person;

                string address;
                Regex addressPattern = new Regex(@"^[A-Za-z\s]+\s\d+[A-Za-z]?$"); 

                do
                {
                    Console.Write("Enter Delivery Address (for example: Boska 25): ");
                    address = Console.ReadLine();
                    if (!addressPattern.IsMatch(address))
                    {
                        Console.Write("Address must be in the format: Street Name + Number");
                        Console.WriteLine();
                    }
                } while (!addressPattern.IsMatch(address));


                string paymentTypeInput;
                PaymentType paymentType;
                do
                {
                    Console.Write("Payment Type (1 = Card, 2 = Cash on Delivery): ");
                    paymentTypeInput = Console.ReadLine();
                    
                    if(paymentTypeInput != "1" && paymentTypeInput != "2")
                    {
                        Console.Write("Wrong input! Choose either 1 or 2.");
                        Console.WriteLine();
                    }
                } while (paymentTypeInput != "1" && paymentTypeInput != "2");

                paymentType = paymentTypeInput == "1" ? PaymentType.Card : PaymentType.CashWhenDelivered;


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
            Console.WriteLine($"|  Price:       {order.Payment.Price} PLN");
            Console.WriteLine($"|  Client Type: {order.Client.ClientType}");
            Console.WriteLine($"|  Address:     {order.Address}");
            Console.WriteLine($"|  Status:      {order.OrderStatus}");
            Console.WriteLine("==============================================\n");
        }


    }
}