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

namespace CashRegister.WPF.ViewModels.Orders.Details
{
    public class OrderDetailsViewModel : Conductor<IOrderColumnView>
    {
        private OrderDetailsProductViewModel _selectedOrderProduct;
        
        private readonly ISessionRegister _sessionRegister;
        private readonly IShellProvider _shellProvider;
        private readonly IBarcodeProducer _barcodeProducer;
        private readonly CurrencySettings _currencySettings;
        private readonly IOrderArchive _orderArchive;

        private OrderDetailsViewModel()
        {
            OrderProducts = new BindableCollection<OrderDetailsProductViewModel>();
        }

        public OrderDetailsViewModel(ISessionRegister sessionRegister,
            IShellProvider shellProvider,
            IBarcodeProducer barcodeProducer,
            IOptions<CurrencySettings> currencyOptions,
            IOrderArchive orderArchive) : this()
        {
            _sessionRegister = sessionRegister;
            _shellProvider = shellProvider;
            _barcodeProducer = barcodeProducer;
            _currencySettings = currencyOptions.Value;
            _orderArchive = orderArchive;
        }

        // public int OrderId { get; set; }
        public OrderSM Order { get; set; }
        public BindableCollection<OrderDetailsProductViewModel> OrderProducts { get; set; }

        public OrderDetailsProductViewModel SelectedOrderProduct
        {
            get => _selectedOrderProduct;
            set
            {
                _selectedOrderProduct = value;
                OpenOrderProductDetailsColumn(value.OrderProduct.Product);
            }
        }

        public decimal Sum => OrderProducts.Sum(x => x.Sum);

        public async void GoBack()
        {
            await _shellProvider.GoBack();
        }

        public async void Receipt(Visual visual)
        {
            await OpenReceiptColumn();
            
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(visual, "Invoice");
            }

            await CloseColumn();
        }

        private Task CloseColumn() => DeactivateItemAsync(ActiveItem, true);

        private Task OpenReceiptColumn() => ActivateItemAsync(new ReceiptColumnViewModel());

        private Task OpenOrderProductDetailsColumn(ProductSM product) =>
            ActivateItemAsync(new OrderProductDetailsColumnViewModel {Product = product});

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            // var order = await _orderArchive.GetOrderAsync(OrderId);
            // var orderProducts =
            //     order.Select(x => new OrderDetailsProductViewModel(x, _barcodeProducer, _currencySettings));
            var orderProducts =
                Order.Select(x => new OrderDetailsProductViewModel(x, _barcodeProducer, _currencySettings));

            OrderProducts.Clear();
            OrderProducts.AddRange(orderProducts);
            NotifyOfPropertyChange(() => Sum);

            await base.OnInitializeAsync(cancellationToken);
        }
    }
}