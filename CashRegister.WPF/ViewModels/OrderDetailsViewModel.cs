using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.ViewModels
{
    public class OrderDetailsViewModel : Screen
    {
        private readonly ISessionRegister _sessionRegister;
        private readonly IBarcodeProducer _barcodeProducer;
        private readonly CurrencySettings _currencySettings;
        private readonly IOrderArchive _orderArchive;

        private OrderDetailsViewModel()
        {
            OrderProducts = new BindableCollection<OrderDetailsProductViewModel>();
        }

        public OrderDetailsViewModel(ISessionRegister sessionRegister,
            IBarcodeProducer barcodeProducer,
            IOptions<CurrencySettings> currencyOptions,
            IOrderArchive orderArchive) : this()
        {
            _sessionRegister = sessionRegister;
            _barcodeProducer = barcodeProducer;
            _currencySettings = currencyOptions.Value;
            _orderArchive = orderArchive;
        }

        // public int OrderId { get; set; }
        public OrderSM Order { get; set; }
        public BindableCollection<OrderDetailsProductViewModel> OrderProducts { get; set; }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            // var order = await _orderArchive.GetOrderAsync(OrderId);
            // var orderProducts =
            //     order.Select(x => new OrderDetailsProductViewModel(x, _barcodeProducer, _currencySettings));
            var orderProducts =
                Order.Select(x => new OrderDetailsProductViewModel(x, _barcodeProducer, _currencySettings));

            OrderProducts.Clear();
            OrderProducts.AddRange(orderProducts);

            await base.OnInitializeAsync(cancellationToken);
        }
    }
}