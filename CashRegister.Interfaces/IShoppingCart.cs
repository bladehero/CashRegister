using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IShoppingCart : IServiceBase
    {
        Task<OrderSM> CreateOrderAsync(SessionSM session);
        Task<OrderSM> AddProductAsync(OrderSM order, string barcode, int quantity = 1);
        Task<OrderSM> ChangeQuantityAsync(OrderSM order, int productId, int quantity);
        Task<OrderSM> RemoveProductAsync(OrderSM order, int productId);
        Task<bool> AcceptOrderAsync(OrderSM order);
    }
}