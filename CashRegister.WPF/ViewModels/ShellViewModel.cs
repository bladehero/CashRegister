using Caliburn.Micro;
using CashRegister.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
    }
}