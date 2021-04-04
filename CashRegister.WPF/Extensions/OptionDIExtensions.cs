using Caliburn.Micro;
using CashRegister.WPF.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF.Extensions
{
    public static class OptionDIExtensions
    {
        public static void AddRegisterOption<T>(this SimpleContainer container) where T : class, new()
        {
            var configuration = container.GetInstance<IConfiguration>();
            var settings = configuration.GetRegisterSettings<T>();
            container.Handler<IOptions<T>>(_ => new RegisterOptions<T>(settings));
        }

        private class RegisterOptions<T> : IOptions<T> where T : class, new()
        {
            public RegisterOptions(T value)
            {
                Value = value;
            }

            public T Value { get; }
        }
    }
}