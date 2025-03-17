using OrderConsoleApp.Enum;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Model
{
    public class Order 
    {
      
        public Guid Id { get; set; }

        public PaymentType Payment { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public ClientType Client { get; set; }
       
        public string Address { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.New;


    }

   


}
