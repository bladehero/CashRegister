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
    public class OrderListViewModel : Screen
    {
        private readonly ISessionRegister _sessionRegister;
        private readonly IOrderArchive _orderArchive;
        private readonly CurrencySettings _currencySettings;

        private OrderListViewModel()
        {
            Orders = new BindableCollection<OrderListRowViewModel>();

            Orders.CollectionChanged += (_, _) => { NotifyOfPropertyChange(() => Sum); };
        }

        public OrderListViewModel(ISessionRegister sessionRegister,
            IOrderArchive orderArchive,
            IOptions<CurrencySettings> currencyOptions) : this()
        {
            _sessionRegister = sessionRegister;
            _orderArchive = orderArchive;
            _currencySettings = currencyOptions.Value;
        }

        public BindableCollection<OrderListRowViewModel> Orders { get; set; }
        public string Sum => $"{Orders.Sum(x => x.SumDecimal)}{_currencySettings.Symbol}";
        public string UserName => _sessionRegister.Current.UserName;
        public string StartedAt => _sessionRegister.Current.Started.ToString("dd MMMM yyyy, hh:mm");

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
                                Barcode = new string(y.ToString()[0], 10),
                                Name = $"{nameof(OrderProductSM)}_{y}",
                                Price = y * y,
                                Id = y,
                                PicturePath = @"C:\Users\nikit\Pictures\Saved Pictures\Wallpaper Nature.jpg"
                            })
                            {
                                Quantity = y
                            })
                    );
                    return new OrderListRowViewModel(_currencySettings, order);
                });

            Orders.Clear();
            Orders.AddRange(orderRows);

            return base.OnInitializeAsync(cancellationToken);
        }
    }
}