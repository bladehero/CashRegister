using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Services
{
    public class OrderArchive : IOrderArchive
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderArchive(IAppDbContext dbContext, IMapperProvider mapperProvider)
        {
            _dbContext = dbContext;
            _mapper = mapperProvider.Mapper;
        }

        public async Task<OrderSM> GetOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product)
                .ThenInclude(x => x.Barcode).Include(x => x.OrderProducts).ThenInclude(x => x.Product)
                .ThenInclude(x => x.Picture).FirstOrDefaultAsync(x => x.Id == orderId);

            var session = _mapper.Map<SessionSM>(_dbContext.Sessions.FirstOrDefault());
            var orderProductSms = order.OrderProducts.Select(x =>
            {
                var orderProductSm = _mapper.Map<OrderProductSM>(x);
                orderProductSm.Product = _mapper.Map<ProductSM>(x.Product);
                return orderProductSm;
            }).ToList();
            return new OrderSM(session, orderProductSms)
            {
                Id = order.Id,
                DateTime = order.DateTime
            };
        }

        public IEnumerable<OrderSM> GetOrders(SessionSM session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var orders = _dbContext.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product)
                .Where(x => x.Session.Id == session.Id)
                .ToList();
            var orderSms = orders.Select(x =>
            {
                var orderProductSms = x.OrderProducts.Select(x =>
                {
                    var orderProductSm = _mapper.Map<OrderProductSM>(x);
                    orderProductSm.Product = _mapper.Map<ProductSM>(x.Product);
                    return orderProductSm;
                }).ToList();
                return new OrderSM(session, orderProductSms)
                {
                    Id = x.Id,
                    DateTime = x.DateTime
                };
            });
            return orderSms;
        }
    }
}