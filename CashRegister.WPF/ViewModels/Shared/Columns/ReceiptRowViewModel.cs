using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Globals;

namespace CashRegister.WPF.ViewModels.Shared.Columns
{
    public class ReceiptRowViewModel
    {
        private readonly OrderProductSM _orderProduct;
        private readonly CurrencySettings _currencySettings;

        public ReceiptRowViewModel(OrderProductSM orderProduct, CurrencySettings currencySettings)
        {
            _orderProduct = orderProduct;
            _currencySettings = currencySettings;
        }

        public string Name => _orderProduct.Product.Name;
        public decimal Price => _orderProduct.Product.Price;
        public decimal TotalPrice => _orderProduct.TotalPrice;
        public string MultiplicationSymbol => Constants.Symbols.Multiplication;
        public int Quantity => _orderProduct.Quantity;
        public string Currency => _currencySettings.Symbol;

        public bool QuantityIsVisible => Quantity > 1;
        public bool TotalPriceIsVisible => QuantityIsVisible;
    }
}