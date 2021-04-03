using System;
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
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            var orderSm = _mapper.Map<OrderSM>(order);
            return orderSm;
        }

        public IEnumerable<OrderSM> GetOrders(SessionSM session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var orders = _dbContext.Orders.Where(x => x.Session.Id == session.Id);
            var orderSms = _mapper.ProjectTo<OrderSM>(orders);
            return orderSms;
        }
    }
}