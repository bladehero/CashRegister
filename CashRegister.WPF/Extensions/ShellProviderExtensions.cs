using System;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using CashRegister.WPF.Interfaces;

namespace CashRegister.WPF.Extensions
{
    public static class ShellProviderExtensions
    {
        public static void RegisterShellProvider(this SimpleContainer container)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var shellType = assembly.DefinedTypes.First(x => x.ImplementedInterfaces.Any(i => i == typeof(IShell)))
                .AsType();
            var shell = container.GetInstance(null, shellType.Name) as IShell;

            container.Handler<IShellProvider>(c =>
            {
                return new ShellProvider(shell);
            });
        }

        private class ShellProvider : IShellProvider
        {
            public ShellProvider(IShell shell)
            {
                Shell = shell;
            }

            public IShell Shell { get; }
        }
    }
}