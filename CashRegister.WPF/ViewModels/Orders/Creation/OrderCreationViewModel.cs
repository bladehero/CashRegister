using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;
using CashRegister.WPF.ViewModels.Shared.Columns;

namespace CashRegister.WPF.ViewModels.Orders.Creation
{
    public class OrderCreationViewModel : Conductor<IOrderColumnView>
    {
        private OrderCreationProductViewModel _selectedOrderProduct;
        private readonly ISessionRegister _sessionRegister;
        private readonly IProductRack _productRack;
        private readonly IWindowManager _windowManager;
        private readonly IShoppingCart _shoppingCart;
        private readonly IShellProvider _shellProvider;
        private readonly IBarcodeProducer _barcodeProducer;
        private readonly CurrencySettings _currencySettings;
        private OrderSM _order;
        private BindableCollection<OrderCreationProductViewModel> _orderProducts;

        public OrderCreationViewModel(ISessionRegister sessionRegister,
            IProductRack productRack,
            IWindowManager windowManager,
            IShoppingCart shoppingCart,
            IShellProvider shellProvider,
            IBarcodeProducer barcodeProducer,
            IOptions<CurrencySettings> currencyOptions)
        {
            _sessionRegister = sessionRegister;
            _productRack = productRack;
            _windowManager = windowManager;
            _shoppingCart = shoppingCart;
            _shellProvider = shellProvider;
            _barcodeProducer = barcodeProducer;
            _currencySettings = currencyOptions.Value;
        }

        public OrderSM Order
        {
            get => _order;
            set
            {
                _order = value;
                NotifyOfPropertyChange(() => OrderProducts);
                NotifyOfPropertyChange(() => Sum);
            }
        }

        public BindableCollection<OrderCreationProductViewModel> OrderProducts
        {
            get
            {
                _orderProducts ??= new BindableCollection<OrderCreationProductViewModel>(GetOrderProducts());

                _orderProducts.Clear();
                _orderProducts.AddRange(GetOrderProducts());
                _orderProducts.Refresh();

                return _orderProducts;
            }
        }

        public bool DeleteProductIsVisible => SelectedOrderProduct is not null;

        public OrderCreationProductViewModel SelectedOrderProduct
        {
            get => _selectedOrderProduct;
            set
            {
                _selectedOrderProduct = value;
                NotifyOfPropertyChange(() => DeleteProductIsVisible);
                if (value is null)
                {
                    CloseColumn();
                }
                else
                {
                    OpenOrderProductDetailsColumn(value.OrderProduct.Product);
                }
            }
        }

        public decimal Sum => OrderProducts?.Sum(x => x.Sum) ?? decimal.Zero;

        public bool ColumnIsVisible => ActiveItem is not null;

        public async void GoBack()
        {
            await _shellProvider.GoBack();
        }

        public async void Receipt(Visual visual)
        {
            await OpenReceiptColumn(Order);

            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(visual, "Invoice");
            }

            await CloseColumn();
        }

        public async void AddProduct()
        {
            var dialog = new ProductByBarcodeViewModel(_productRack);
            var result = await _windowManager.ShowDialogAsync(dialog);
            if (result.GetValueOrDefault())
            {
                Order = await _shoppingCart.AddProductAsync(Order, dialog.Product.Barcode);
            }
        }

        public async void DeleteProduct()
        {
            Order = await _shoppingCart.RemoveProductAsync(Order, SelectedOrderProduct.OrderProduct.Product.Id);
        }

        public async void AcceptOrder()
        {
            var result = await _shoppingCart.AcceptOrderAsync(Order);
            await _shellProvider.GoBack();
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var session = _sessionRegister.Current;
            Order = await _shoppingCart.CreateOrderAsync(session);
            await base.OnInitializeAsync(cancellationToken);
        }

        #region Helpers

        private IEnumerable<OrderCreationProductViewModel> GetOrderProducts() => _order?.Select(x =>
                new OrderCreationProductViewModel(x, _barcodeProducer, _currencySettings)) ??
            Array.Empty<OrderCreationProductViewModel>();

        private Task CloseColumn() => DeactivateItemAsync(ActiveItem, true);

        private Task OpenReceiptColumn(OrderSM order) =>
            ActivateItemAsync(new ReceiptColumnViewModel(order, _currencySettings));

        private Task OpenOrderProductDetailsColumn(ProductSM product) =>
            ActivateItemAsync(new OrderProductDetailsColumnViewModel {Product = product});

        #endregion
    }
}