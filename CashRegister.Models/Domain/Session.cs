using System;
using System.Collections.Generic;

namespace CashRegister.Models.Domain
{
    public class Session
    {
        public Session()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }

        public IList<Order> Orders { get; set; }
    }
}