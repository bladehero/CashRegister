using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Services
{
    public class ProductRack : IProductRack
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRack(IAppDbContext dbContext, IMapperProvider mapperProvider)
        {
            _dbContext = dbContext;
            _mapper = mapperProvider.Mapper;
        }

        public async Task<ProductSM> GetAsync(string barcode)
        {
            var query = _dbContext.Products.Include(x => x.Barcode);
            var product = await query.SingleOrDefaultAsync(x => x.Barcode.Value == barcode);
            var productSm = _mapper.Map<ProductSM>(product);
            return productSm;
        }
    }
}