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
                config.CreateMap<User, UserSM>().ReverseMap();
                config.CreateMap<Session, SessionSM>().ReverseMap();

                config.CreateMap<ProductSM, Product>().AfterMap((sm, m) =>
                {
                    m.Barcode = new Barcode()
                    {
                        Id = sm.BarcodeId,
                        Value = sm.Barcode
                    };
                    m.Picture = new Picture()
                    {
                        Id = sm.PictureId,
                        Path = sm.PicturePath
                    };
                }).ReverseMap().AfterMap((m, sm) =>
                {
                    sm.BarcodeId = m.Barcode?.Id ?? default;
                    sm.Barcode = m.Barcode?.Value;
                    sm.PictureId = m.Picture?.Id ?? default;
                    sm.PicturePath = m.Picture?.Path;
                });

                config.CreateMap<OrderProduct, OrderProductSM>().ReverseMap();

                config.CreateMap<Order, OrderSM>().ReverseMap();
            });
        }

        public IMapper Mapper => _configurationProvider.CreateMapper();
    }
}