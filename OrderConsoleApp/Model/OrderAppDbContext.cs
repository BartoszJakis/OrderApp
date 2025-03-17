using Microsoft.EntityFrameworkCore;
using OrderConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Model
{
    public class OrderAppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderAppDbContext(DbContextOptions<OrderAppDbContext> options) : base(options)
        {
        }

        public OrderAppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
           .HasKey(o => o.Id);
        }
    }
}