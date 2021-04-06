using CashRegister.Models.Services;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.ViewModels.Shared.Columns
{
    public class OrderProductDetailsColumnViewModel : IOrderColumnView
    {
        public ProductSM Product { get; set; }
    }
}