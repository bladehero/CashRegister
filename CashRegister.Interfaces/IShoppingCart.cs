using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IShoppingCart
    {
        Task<OrderSM> CreateOrder();
        Task<OrderSM> AddProduct(OrderSM order, string barcode, int quantity = 1);
        Task<OrderSM> ChangeQuantity(OrderSM order, int productId, int quantity);
        Task<OrderSM> RemoveProduct(OrderSM order, int productId);
        Task<bool> AcceptOrder(OrderSM order);
    }
}