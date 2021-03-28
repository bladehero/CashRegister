using System.Threading.Tasks;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Extensions;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.ViewModels
{
    public class SessionViewModel : Caliburn.Micro.Screen
    {
        private readonly IAppDbContext _dbContext;
        private readonly BalanceRange _balanceRange;

        public SessionViewModel(IAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _balanceRange = configuration.GetRegisterSettings<BalanceRange>();
        }

        public UserSM User { get; set; }

        public decimal? Balance { get; set; }

        public bool CanInitialize(decimal? balance)
        {
            return balance.HasValue && _balanceRange.IsIncluded(balance.Value);
        }
        
        public async void Initialize(decimal? balance)
        {
            await Task.CompletedTask;
        }
    }
}