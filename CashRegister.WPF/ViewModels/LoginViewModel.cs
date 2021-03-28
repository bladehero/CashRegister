using System;
using System.Threading.Tasks;
using CashRegister.Interfaces;
using CashRegister.Models.Services;

namespace CashRegister.WPF.ViewModels
{
    public class LoginViewModel : Caliburn.Micro.Screen
    {
        private readonly IUserStorage _userStorage;

        public LoginViewModel(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public event Func<UserSM, Task> Logged;

        public bool CanLogin(string user, string password)
        {
            return !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password);
        }

        public async void Login(string user, string password)
        {
            if (Logged is null)
                return;

            var userSm = _userStorage.GetUserByCredentials(user, password);
            if (userSm is null)
                return;

            await Logged(userSm);
        }
    }
}