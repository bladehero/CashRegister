using System;
using CashRegister.Models.Services;

namespace CashRegister.Interfaces
{
    public interface IUserStorage : IServiceBase
    {
        UserSM GetUserByGuid(Guid guid);
        UserSM GetUserByGuid(string guid);
        UserSM GetUserByCredentials(string userName, string password);
        bool Exists(string userName);
    }
}