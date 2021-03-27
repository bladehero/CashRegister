using System.IO;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.Extensions
{
    public static class ConfigurationFactory
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}