using System;

namespace CashRegister.Models.Services
{
    public class SessionSM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
    }
}