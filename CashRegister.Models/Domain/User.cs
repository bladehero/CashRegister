using System;

namespace CashRegister.Models.Domain
{
    public class User
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}