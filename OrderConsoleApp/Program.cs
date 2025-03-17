using Microsoft.EntityFrameworkCore;
using OrderConsoleApp.Enum;
using OrderConsoleApp.Interaction;
using OrderConsoleApp.Model;
using OrderConsoleApp.Repostiory;
using OrderConsoleApp.Services;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderAppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=postgres"); 

        using (var dbContext = new OrderAppDbContext(optionsBuilder.Options))
        {
            IOrderRepository orderRepository = new OrderRepository(dbContext);
            IOrderService orderService = new OrderService(orderRepository);
            IOrderConsoleUI ui = new OrderConsoleUI(orderService);
            await ui.DisplayMenu();

        }

    }
    
}
