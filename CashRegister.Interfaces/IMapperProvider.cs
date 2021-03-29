using AutoMapper;

namespace CashRegister.Interfaces
{
    public interface IMapperProvider : IServiceBase
    {
        IMapper Mapper { get; }
    }
}