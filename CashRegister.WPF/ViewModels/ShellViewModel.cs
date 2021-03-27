using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly LoginViewModel _loginViewModel;

        public ShellViewModel(IWindowManager windowManager, LoginViewModel loginViewModel)
        {
            _windowManager = windowManager;
            _loginViewModel = loginViewModel;
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_loginViewModel, cancellationToken);
            await base.OnInitializeAsync(cancellationToken);
        }
    }
}