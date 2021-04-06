using System;
using System.Linq;
using Caliburn.Micro;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.ViewModels.Shared.Columns
{
    public class ReceiptColumnViewModel : IOrderColumnView
    {
        private readonly OrderSM _order;
        private readonly CurrencySettings _currencySettings;

        public ReceiptColumnViewModel(OrderSM order, CurrencySettings currencySettings)
        {
            _order = order;
            _currencySettings = currencySettings;

            Rows = new BindableCollection<ReceiptRowViewModel>(_order.Select(x => new ReceiptRowViewModel(x, currencySettings)));
        }

        public BindableCollection<ReceiptRowViewModel> Rows { get; }
        public DateTime DateTime => _order.DateTime;
        public decimal Sum => _order.Sum;
        public string Currency => _currencySettings.Symbol;
        public string ShopAssistant => _order.Session.FullName;
    }
}