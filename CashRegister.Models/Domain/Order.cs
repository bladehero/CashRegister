using System;
using System.Collections.Generic;

namespace CashRegister.Models.Domain
{
    public class Order
    {
        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }
        
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public IList<OrderProduct> OrderProducts { get; set; }
    }
}