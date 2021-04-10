using Caliburn.Micro;
using CashRegister.Interfaces;
using CashRegister.Models.Services;

namespace CashRegister.WPF.ViewModels.Orders.Creation
{
    public class ProductByBarcodeViewModel : Screen
    {
        private readonly IProductRack _productRack;
        private string _barcode;

        public ProductByBarcodeViewModel(IProductRack productRack)
        {
            _productRack = productRack;
        }

        public ProductSM Product { get; set; }

        public string Barcode
        {
            get => _barcode;
            set
            {
                _barcode = value;
                var product = _productRack.GetAsync(Barcode).Result;
                Product = product;
                NotifyOfPropertyChange(() => Product);
            }
        }

        public async void Cancel()
        {
            await TryCloseAsync(false);
        }

        public async void Ok()
        {
            if (Product is null)
            {
                await TryCloseAsync(false);
            }

            await TryCloseAsync(true);
        }
    }
}