using System.Threading.Tasks;
using CashRegister.Interfaces;
using CashRegister.Models.Services;

namespace CashRegister.Services
{
    public class SessionRegister : ISessionRegister
    {
        public SessionSM Current { get; }
        
        public Task StartAsync(string user, decimal balance)
        {
            throw new System.NotImplementedException();
        }

        public Task FinishAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}