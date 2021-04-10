using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.WPF.Attributes;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    [LifetimeScope(LifetimeScope.Singletone)]
    public class ShellViewModel : Conductor<object>, IShell
    {
        private readonly IShellProvider _shellProvider;

        public ShellViewModel(IShellProvider shellProvider)
        {
            _shellProvider = shellProvider;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return _shellProvider.GotoAsync<LoginViewModel>(cancellationToken);
        }
    }
}