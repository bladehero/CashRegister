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
        
        
    }
}