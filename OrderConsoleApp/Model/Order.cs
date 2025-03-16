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

        public Payment Payment { get; set; }

        public string ProductName { get; set; }

        public Client Client { get; set; }

      
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Address can only contain letters, numbers, spaces, commas, periods, and hyphens.")]
        public string Address { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.New;


    }

   


}
