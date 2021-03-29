using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly ISessionRegister _sessionRegister;
        private readonly LoginViewModel _loginViewModel;
        private readonly SessionViewModel _sessionViewModel;

        public ShellViewModel(IWindowManager windowManager,
            ISessionRegister sessionRegister,
            LoginViewModel loginViewModel,
            SessionViewModel sessionViewModel)
        {
            _windowManager = windowManager;
            _sessionRegister = sessionRegister;
            _loginViewModel = loginViewModel;
            _sessionViewModel = sessionViewModel;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _loginViewModel.Logged += x =>
            {
                if (_sessionRegister.HasCurrent == false)
                {
                    _sessionViewModel.User = x;
                    return ActivateItemAsync(_sessionViewModel, cancellationToken);
                }

                if (_sessionRegister.Current.UserName == x.UserName)
                {
                    return ActivateItemAsync(null, cancellationToken);
                }

                return Task.CompletedTask;
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