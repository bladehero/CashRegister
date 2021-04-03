using System;
using System.Linq;
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

        public Task<OrderSM> CreateOrder(SessionSM session)
        {
            return Task.FromResult(OrderFactory.GetEmptyOrder(session));
        }

        public async Task<OrderSM> AddProduct(OrderSM order, string barcode, int quantity = 1)
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

            var orderProductSm = order.FirstOrDefault(x => x.Product.Barcode == barcode);

            if (orderProductSm is null)
            {
                var productSm = await _productRack.Get(barcode);
                orderProductSm = new OrderProductSM(productSm)
                {
                    Quantity = quantity
                };
            }
            else
            {
                orderProductSm.Quantity += quantity;
            }

            var orderSm = new OrderSM(order.Session, order.Append(orderProductSm));
            orderProductSm.Order = orderSm;
            return orderSm;
        }

        public Task<OrderSM> ChangeQuantity(OrderSM order, int productId, int quantity)
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

        public Task<OrderSM> RemoveProduct(OrderSM order, int productId)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var products = order.Where(x => x.Product?.Id != productId);
            return Task.FromResult(new OrderSM(order.Session, products));
        }

        public async Task<bool> AcceptOrder(OrderSM order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            
            var model = _mapper.Map<Order>(order);
            var entity = await _dbContext.Orders.AddAsync(model);
            return entity.State == EntityState.Added;
        }
    }

    internal class OrderFactory
    {
        public static OrderSM GetEmptyOrder(SessionSM session) => new(session) {DateTime = DateTime.UtcNow};
    }
}