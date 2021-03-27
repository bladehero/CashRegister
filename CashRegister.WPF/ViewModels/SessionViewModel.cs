using CashRegister.Interfaces;
using CashRegister.Models.Domain;

namespace CashRegister.WPF.ViewModels
{
    public class SessionViewModel : Caliburn.Micro.Screen
    {
        private readonly IAppDbContext _dbContext;

        public SessionViewModel(IAppDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbContext.Barcodes.Add(new Barcode() {Value = "123123123123"});
        }
    }
}