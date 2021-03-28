using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly LoginViewModel _loginViewModel;
        private readonly SessionViewModel _sessionViewModel;

        public ShellViewModel(IWindowManager windowManager,
            LoginViewModel loginViewModel,
            SessionViewModel sessionViewModel)
        {
            _windowManager = windowManager;
            _loginViewModel = loginViewModel;
            _sessionViewModel = sessionViewModel;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _loginViewModel.Logged += x =>
            {
                _sessionViewModel.User = x;
                return ActivateItemAsync(_sessionViewModel, cancellationToken);
            };

            await base.OnActivateAsync(cancellationToken);
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_loginViewModel, cancellationToken);
            await base.OnInitializeAsync(cancellationToken);
        }
    }
}