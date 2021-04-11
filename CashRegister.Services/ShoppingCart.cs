using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Domain;
using CashRegister.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IAppDbContext _dbContext;
        private readonly IProductRack _productRack;
        private readonly IMapper _mapper;

        public ShoppingCart(IAppDbContext dbContext, IMapperProvider mapperProvider, IProductRack productRack)
        {
            _dbContext = dbContext;
            _productRack = productRack;
            _mapper = mapperProvider.Mapper;
        }

        public Task<OrderSM> CreateOrderAsync(SessionSM session)
        {
            return Task.FromResult(OrderFactory.GetEmptyOrder(session));
        }

        public async Task<OrderSM> AddProductAsync(OrderSM order, string barcode, int quantity = 1)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (barcode == null)
            {
                throw new ArgumentNullException(nameof(barcode));
            }

            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity should be more positive number.");
            }

            OrderSM orderSm;
            var orderProductSm = order.FirstOrDefault(x => x.Product.Barcode == barcode);
            if (orderProductSm is null)
            {
                var productSm = await _productRack.GetAsync(barcode);
                if (productSm is null)
                {
                    return order;
                }

                orderProductSm = new OrderProductSM(productSm)
                {
                    Quantity = quantity
                };
                orderSm = new OrderSM(order.Session, order.Append(orderProductSm));
            }
            else
            {
                orderProductSm.Quantity += quantity;
                orderSm = new OrderSM(order.Session, order);
            }

            orderProductSm.Order = orderSm;
            return orderSm;
        }

        public Task<OrderSM> ChangeQuantityAsync(OrderSM order, int productId, int quantity)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (quantity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity should be more positive number.");
            }

            var product = order.FirstOrDefault(x => x.Product?.Id == productId);
            if (product != null) product.Quantity = quantity;
            return Task.FromResult(order);
        }

        public Task<OrderSM> RemoveProductAsync(OrderSM order, int productId)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var products = order.Where(x => x.Product?.Id != productId);
            return Task.FromResult(new OrderSM(order.Session, products));
        }

        public async Task<bool> AcceptOrderAsync(OrderSM orderSm)
        {
            if (orderSm == null)
            {
                throw new ArgumentNullException(nameof(orderSm));
            }

            var session = await _dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == orderSm.Session.Id);

            var order = new Order
            {
                Session = session,
                DateTime = orderSm.DateTime
            };
            var query = _dbContext.Products;
            var products = orderSm.Join(query, x => x.Product.Id, x => x.Id, (o, p) => new OrderProduct
            {
                Order = order,
                Product = p,
                Quantity = o.Quantity
            }).Where(x => x.Quantity > 0).ToList();

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.OrderProducts.AddRangeAsync(products);

            var count = await _dbContext.SaveChangesAsync(CancellationToken.None);
            return count > 0;
        }
    }

    internal class OrderFactory
    {
        public static OrderSM GetEmptyOrder(SessionSM session) => new(session) {DateTime = DateTime.UtcNow};
    }
}