using System;
using System.Threading.Tasks;
using CashRegister.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class LoginViewModel : Caliburn.Micro.Screen
    {
        private readonly IUserStorage _userStorage;

        public LoginViewModel(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public event Func<Task> Logged;

        public bool CanLogin(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        public async void Login(string user, string password)
        {
            if (_userStorage.GetUserByCredentials(user, password) is null)
                return;

            if (Logged is null)
                return;

            await Logged();
        }
    }
}