using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IUserStorage
    {
        UserSM GetUserByCredentials(string userName, string password);
    }
}