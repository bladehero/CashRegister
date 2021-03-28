using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface ISessionRegister
    {
        SessionSM Current { get; }
        Task StartAsync(string user, decimal balance);
        Task FinishAsync();
    }
}