using OrderConsoleApp.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Model
{
    public class Payment
    {
        public float Price { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
