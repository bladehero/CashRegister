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
        private readonly ISessionRegister _sessionRegister;
        private readonly IShellProvider _shellProvider;
        private readonly IBarcodeProducer _barcodeProducer;
        private readonly CurrencySettings _currencySettings;
        private readonly IOrderArchive _orderArchive;
        private OrderDetailsProductViewModel _selectedOrderProduct;
        private BindableCollection<OrderDetailsProductViewModel> _orderProducts;
        private BindableCollection<ReceiptRowViewModel> _receiptRows;

        private OrderDetailsViewModel()
        {
            _orderProducts = new BindableCollection<OrderDetailsProductViewModel>();
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

        public BindableCollection<OrderDetailsProductViewModel> OrderProducts => _orderProducts;

        public OrderDetailsProductViewModel SelectedOrderProduct
        {
            get => _selectedOrderProduct;
            set
            {
                Set(ref _selectedOrderProduct, value);
                NotifyOfPropertyChange(() => DetailSpaceIsVisible);
            }
        }

        public bool DetailSpaceIsVisible => _selectedOrderProduct is not null && ReceiptIsVisible == false;
        public bool ReceiptIsVisible { get; set; }
        public decimal Sum => OrderProducts.Sum(x => x.Sum);

        public async void BackToOrders()
        {
            await _shellProvider.GoBack();
        }

        public void Receipt(Visual visual)
        {
            ReceiptIsVisible = true;
            SelectedOrderProduct = null;
            
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(visual, "Invoice");
            }
            
            ReceiptIsVisible = false;
        }
        
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