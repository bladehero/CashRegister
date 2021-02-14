using AutoMapper;

namespace CashRegister.Interfaces
{
    public interface IMapperProvider
    {
        IMapper Mapper { get; }
    }
}