using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.WPF.Interfaces;
using CashRegister.WPF.ViewModels;

namespace CashRegister.WPF.Extensions
{
    public static class ShellProviderExtensions
    {
        public static void RegisterShellProvider(this SimpleContainer simpleContainer)
        {
            simpleContainer.Handler<IShellProvider>(currentContainer => new ShellProvider(currentContainer));
        }

        private class ShellProvider : IShellProvider
        {
            private readonly SimpleContainer _container;
            private IShell _shell;

            public ShellProvider(SimpleContainer container)
            {
                _container = container;
            }

            public IShell Shell => _shell ??= _container.GetInstance<ShellViewModel>();


            public Task GotoAsync<T>(CancellationToken cancellationToken = default) where T : IScreen
            {
                var screen = _container.GetInstance<T>();
                return Shell.ActivateItemAsync(screen, cancellationToken);
            }
            public Task GotoAsync<T>(Action<T> screenMutator, CancellationToken cancellationToken = default) where T : IScreen
            {
                if (screenMutator == null) throw new ArgumentNullException(nameof(screenMutator));
                
                var screen = _container.GetInstance<T>();
                screenMutator(screen);
                return Shell.ActivateItemAsync(screen, cancellationToken);
            }
        }
    }
}