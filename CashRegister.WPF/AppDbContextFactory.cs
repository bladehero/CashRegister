using System.IO;
using CashRegister.Data;
using CashRegister.WPF.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connection = ConfigurationFactory.GetConfiguration().GetConnectionString("DefaultConnection");
            
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var options = optionsBuilder.UseSqlite(connection).Options;
            
            return new AppDbContext(options);
        }
    }
}