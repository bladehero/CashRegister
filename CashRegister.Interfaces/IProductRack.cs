using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IProductRack : IServiceBase
    {
        Task<ProductSM> GetAsync(string barcode);
    }
}