using System.Threading.Tasks;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IProductRack
    {
        Task<ProductSM> Get(string barcode);
    }
}