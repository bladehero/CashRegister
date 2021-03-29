using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Extensions;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.ViewModels
{
    public class SessionViewModel : Caliburn.Micro.Screen
    {
        private readonly ISessionRegister _sessionRegister;
        private readonly BalanceRange _balanceRange;

        public SessionViewModel(IConfiguration configuration, ISessionRegister sessionRegister)
        {
            _sessionRegister = sessionRegister;
            _balanceRange = configuration.GetRegisterSettings<BalanceRange>();
            
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
        }
    }
}