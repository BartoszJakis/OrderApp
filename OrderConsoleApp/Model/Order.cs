using OrderConsoleApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public Payment Payment { get; set; }

        public string nameOfProduct { get; set; }

        public Client Client { get; set; }

        public string Address { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.New;


    }
}
