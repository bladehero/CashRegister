using CashRegister.Interfaces;
using CashRegister.WPF.Interfaces;
using CashRegister.WPF.ViewModels.Orders;

namespace CashRegister.WPF.ViewModels
{
    public class LoginViewModel : Caliburn.Micro.Screen
    {
        private readonly IUserStorage _userStorage;
        private readonly ISessionRegister _sessionRegister;
        private readonly IShellProvider _shellProvider;

        public LoginViewModel(IUserStorage userStorage, ISessionRegister sessionRegister, IShellProvider shellProvider)
        {
            _userStorage = userStorage;
            _sessionRegister = sessionRegister;
            _shellProvider = shellProvider;
        }

        public bool CanLogin(string user, string password)
        {
            return !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password);
        }

        public async void Login(string user, string password)
        {
            var userSm = _userStorage.GetUserByCredentials(user, password);
            if (userSm is null)
                return;

            if (_sessionRegister.HasCurrent == false)
            {
                await _shellProvider.GotoAsync<SessionViewModel>(x => x.User = userSm);
            }
            else if (_sessionRegister.Current.UserName == userSm.UserName)
            {
                await _shellProvider.GotoAsync<OrderListViewModel>();
            }
        }
    }
}