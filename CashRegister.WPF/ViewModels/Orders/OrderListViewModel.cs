using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using CashRegister.Models.Settings;
using CashRegister.WPF.Interfaces;
using CashRegister.WPF.ViewModels.Orders.Creation;
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
            }
        }

        public string Sum => $"{Orders.Sum(x => x.SumDecimal)}{_currencySettings.Symbol}";
        public string UserName => _sessionRegister.Current.UserName;
        public string StartedAt => _sessionRegister.Current.Started.ToString("dd MMMM yyyy, hh:mm");

        public bool SelectedOrderIsVisible => _selectedOrder is not null;
        public bool AddIsVisible => true;
        public bool DetailsIsVisible => _selectedOrder is not null;

        public async void Add()
        {
            await _shellProvider.GotoAsync<OrderCreationViewModel>();
        }
        
        public async void Details()
        {
            var orderId = SelectedOrder.OrderId;
            await _shellProvider.GotoAsync<OrderDetailsViewModel>(x => x.OrderId = orderId);
        }

        public async void FinishSession()
        {
            await _sessionRegister.FinishAsync();
            await _shellProvider.GotoAsync<LoginViewModel>();
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var orders = _orderArchive.GetOrders(_sessionRegister.Current);
            var orderRows = orders.Select(x => new OrderListRowViewModel(_currencySettings, x));

            Orders.Clear();
            Orders.AddRange(orderRows);

            return base.OnInitializeAsync(cancellationToken);
        }
    }
}