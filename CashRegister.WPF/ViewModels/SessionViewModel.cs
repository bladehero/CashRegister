using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class SessionViewModel : Caliburn.Micro.Screen
    {
        private readonly IShellProvider _shellProvider;
        private readonly ISessionRegister _sessionRegister;
        private readonly BalanceRange _balanceRange;

        public SessionViewModel(IShellProvider shellProvider,
            ISessionRegister sessionRegister,
            IOptions<BalanceRange> balanceRangeOptions)
        {
            _shellProvider = shellProvider;
            _sessionRegister = sessionRegister;
            _balanceRange = balanceRangeOptions.Value;

            Balance = _balanceRange.Start;
        }

        public UserSM User { get; set; }
        public decimal? Balance { get; set; }

        public bool CanInitialize(decimal? balance)
        {
            return balance.HasValue && _balanceRange.IsIncluded(balance.Value);
        }

        public async void Initialize(decimal balance)
        {
            await _sessionRegister.StartAsync(User?.UserName, balance);
            await _shellProvider.GotoAsync<OrderListViewModel>();
        }
    }
}