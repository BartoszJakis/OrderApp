using OrderConsoleApp.Enum;
using OrderConsoleApp.Interaction;
using OrderConsoleApp.Model;
using OrderConsoleApp.Repostiory;
using OrderConsoleApp.Services;
using System;

class Program
{
    static void Main()
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderService orderService = new OrderService(orderRepository);
        IOrderConsoleUI ui = new OrderConsoleUI(orderService);
        ui.DisplayMenu();
       
    }
    
}
