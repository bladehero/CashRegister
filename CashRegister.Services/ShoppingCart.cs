using System;
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
        private readonly IMapper _mapper;

        public ShoppingCart(IAppDbContext dbContext, IMapperProvider mapperProvider)
        {
            _dbContext = dbContext;
            _mapper = mapperProvider.Mapper;
        }


        public Task<OrderSM> CreateOrder()
        {
            return Task.FromResult(OrderFactory.EmptyOrder);
        }

        public async Task<OrderSM> AddProduct(OrderSM order, string barcode, int quantity = 1)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OrderSM> ChangeQuantity(OrderSM order, int productId, int quantity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OrderSM> RemoveProduct(OrderSM order, int productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OrderSM> CancelOrder(OrderSM order)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AcceptOrder(OrderSM order)
        {
            var model = _mapper.Map<Order>(order);
            var entity = await _dbContext.Orders.AddAsync(model);
            return entity.State == EntityState.Added;
        }
    }

    internal class OrderFactory
    {
        public static OrderSM EmptyOrder => new() {DateTime = DateTime.UtcNow};
    } 
}