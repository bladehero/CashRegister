using System.Collections.Generic;
using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IOrderArchive : IServiceBase
    {
        Task<OrderSM> GetOrderAsync(int orderId);
        IEnumerable<OrderSM> GetOrders(SessionSM session);
    }
}