using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;
using CashRegister.WPF.ViewModels.Orders.Details;

namespace CashRegister.WPF.ViewModels.Orders
{
    public class OrderListViewModel : Screen
    {
        private readonly ISessionRegister _sessionRegister;
        private readonly IShellProvider _shellProvider;
        private readonly IOrderArchive _orderArchive;
        private readonly CurrencySettings _currencySettings;
        private OrderListRowViewModel _selectedOrder;

        private OrderListViewModel()
        {
            Orders = new BindableCollection<OrderListRowViewModel>();

            Orders.CollectionChanged += (_, _) => { NotifyOfPropertyChange(() => Sum); };
        }

        public OrderListViewModel(ISessionRegister sessionRegister,
            IShellProvider shellProvider,
            IOrderArchive orderArchive,
            IOptions<CurrencySettings> currencyOptions) : this()
        {
            _sessionRegister = sessionRegister;
            _shellProvider = shellProvider;
            _orderArchive = orderArchive;
            _currencySettings = currencyOptions.Value;
        }

        public BindableCollection<OrderListRowViewModel> Orders { get; }

        public OrderListRowViewModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                Set(ref _selectedOrder, value);
                NotifyOfPropertyChange(() => SelectedOrderIsVisible);
                NotifyOfPropertyChange(() => DetailsIsVisible);
                NotifyOfPropertyChange(() => DeleteIsVisible);
            }
        }

        public string Sum => $"{Orders.Sum(x => x.SumDecimal)}{_currencySettings.Symbol}";
        public string UserName => _sessionRegister.Current.UserName;
        public string StartedAt => _sessionRegister.Current.Started.ToString("dd MMMM yyyy, hh:mm");

        public bool SelectedOrderIsVisible => _selectedOrder is not null;
        public bool AddIsVisible => true;
        public bool DetailsIsVisible => _selectedOrder is not null;
        public bool DeleteIsVisible => _selectedOrder is not null;

        public async void Details()
        {
            // var orderId = SelectedOrder.OrderId;
            // await _shellProvider.GotoAsync<OrderDetailsViewModel>(x => x.OrderId = orderId);
            await _shellProvider.GotoAsync<OrderDetailsViewModel>(x => x.Order = SelectedOrder.Order);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var orders = _orderArchive.GetOrders(_sessionRegister.Current);
            // var orderRows = orders.Select(x => new OrderListRowViewModel(_currencySettings, x));
            var orderRows =
                Enumerable.Range(1, 10).Select(x =>
                {
                    var order = new OrderSM(_sessionRegister.Current,
                        Enumerable.Range(0, x)
                            .Select(y => new OrderProductSM(new ProductSM
                            {
                                Barcode = new string(y.ToString()[0], 12),
                                Name = $"{nameof(OrderProductSM)}_{y}",
                                Price = y * y,
                                Id = y,
                                PicturePath = @"C:\Users\nikit\Pictures\Saved Pictures\Wallpaper Nature.jpg"
                            })
                            {
                                Quantity = y
                            })
                    )
                    {
                        Id = x
                    };
                    return new OrderListRowViewModel(_currencySettings, order);
                });

            Orders.Clear();
            Orders.AddRange(orderRows);

            return base.OnInitializeAsync(cancellationToken);
        }
    }
}