using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;

namespace CashRegister.WPF.ViewModels.Orders.Details
{
    public class OrderDetailsProductViewModel
    {
        private readonly OrderProductSM _orderProduct;
        private readonly IBarcodeProducer _barcodeProducer;
        private readonly CurrencySettings _currencySettings;

        public OrderDetailsProductViewModel(OrderProductSM orderProduct,
            IBarcodeProducer barcodeProducer,
            CurrencySettings currencySettings)
        {
            _orderProduct = orderProduct;
            _barcodeProducer = barcodeProducer;
            _currencySettings = currencySettings;
        }

        public OrderProductSM OrderProduct => _orderProduct;

        public ImageSource Barcode
        {
            get
            {
                using var ms = new MemoryStream();
                try
                {
                    var image = _barcodeProducer.GenerateImage(_orderProduct.Product.Barcode);
                    image.Save(ms, ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;
            }
        }

        public string BarcodeNumber => _orderProduct.Product.Barcode;

        public string Name => _orderProduct.Product.Name;
        public int Quantity => _orderProduct.Quantity;
        public string Currency => _currencySettings.Symbol;
        public decimal Price => _orderProduct.Product.Price;
        public decimal Sum => Quantity * Price;
    }
}