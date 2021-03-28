using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string SectionRegisterSettings = "RegisterSettings";

        private static IConfigurationSection GetRegisterSettings(this IConfiguration configuration)
        {
            return configuration.GetSection(SectionRegisterSettings);
        }

        public static T GetRegisterSettings<T>(this IConfiguration configuration) where T : class, new()
        {
            var instance = new T();
            var section = typeof(T).Name;
            GetRegisterSettings(configuration).GetSection(section).Bind(instance);
            return instance;
        }
    }
}