using CashRegister.Models.Services;
using CashRegister.Models.Settings;

namespace CashRegister.WPF.ViewModels
{
    public class OrderListRowViewModel
    {
        private readonly CurrencySettings _currencyOptions;
        private readonly OrderSM _order;

        public OrderListRowViewModel(CurrencySettings currencyOptions, OrderSM order)
        {
            _currencyOptions = currencyOptions;
            _order = order;
        }

        public int OrderId => _order.Id;
        public string UserName => _order.Session.UserName;
        public string DateTime => _order.DateTime.ToString("dd MMMM yyyy, hh:mm");
        public string Sum => $"{_order.Sum}{_currencyOptions.Symbol}";
        public decimal SumDecimal => _order.Sum;
        public string Count => _order.Count.ToString();
    }
}