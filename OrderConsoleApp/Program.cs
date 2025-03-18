using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        var serviceProvider = new ServiceCollection()
          .AddDbContext<OrderAppDbContext>(options =>
              options.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=postgres"))
          .AddScoped<IOrderRepository, OrderRepository>()
          .AddScoped<IOrderService, OrderService>()
          .AddScoped<IOrderConsoleUI, OrderConsoleUI>()
          .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope()) 
        {
            var ui = scope.ServiceProvider.GetRequiredService<IOrderConsoleUI>();
            await ui.DisplayMenu();
        }

    }
    
}
