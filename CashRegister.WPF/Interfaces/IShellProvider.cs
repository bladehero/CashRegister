using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace CashRegister.WPF.Interfaces
{
    public interface IShellProvider
    {
        IShell Shell { get; }
        Task GotoAsync<T>(CancellationToken cancellationToken = default) where T : IScreen;
        Task GotoAsync<T>(Action<T> screenMutator, CancellationToken cancellationToken = default) where T : IScreen;
        Task GoBack(CancellationToken cancellationToken = default);
    }
}