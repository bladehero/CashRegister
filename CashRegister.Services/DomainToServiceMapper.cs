using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Domain;
using CashRegister.Models.Services;

namespace CashRegister.Services
{
    public class DomainToServiceMapper : IMapperProvider
    {
        private readonly IConfigurationProvider _configurationProvider;

        public DomainToServiceMapper()
        {
            _configurationProvider = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserSM>();
                config.CreateMap<Session, SessionSM>();
            });
        }

        public IMapper Mapper => _configurationProvider.CreateMapper();
    }
}