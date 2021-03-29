using System.Threading;
using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface ISessionRegister : IServiceBase
    {
        bool HasCurrent { get; }
        SessionSM Current { get; }
        Task StartAsync(string user, decimal balance, CancellationToken cancellationToken = default);
        Task FinishAsync(CancellationToken cancellationToken = default);
    }
}