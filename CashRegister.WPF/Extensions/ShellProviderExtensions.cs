using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CashRegister.WPF.Attributes;
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
            private static readonly Stack<IScreen> _history;
            private readonly SimpleContainer _container;
            private IShell _shell;

            static ShellProvider()
            {
                _history = new Stack<IScreen>();
            }

            public ShellProvider(SimpleContainer container)
            {
                _container = container;
            }

            public IShell Shell => _container.GetInstance<ShellViewModel>();


            public Task GotoAsync<T>(CancellationToken cancellationToken = default) where T : IScreen
            {
                var screen = _container.GetInstance<T>();
                _history.Push(screen);
                return Shell.ActivateItemAsync(screen, cancellationToken);
            }

            public Task GotoAsync<T>(Action<T> screenMutator, CancellationToken cancellationToken = default)
                where T : IScreen
            {
                if (screenMutator == null) throw new ArgumentNullException(nameof(screenMutator));

                var screen = _container.GetInstance<T>();
                screenMutator(screen);
                _history.Push(screen);
                return Shell.ActivateItemAsync(screen, cancellationToken);
            }

            public Task GoBack(CancellationToken cancellationToken = default)
            {
                _history.Pop();
                var screen = _history.Peek();
                var screenType = screen.GetType();
                switch (screenType.GetCustomAttribute<LifetimeScopeAttribute>()?.LifetimeScope)
                {
                    case LifetimeScope.Singletone:
                        break;
                    default:
                        screen = _container.GetInstance(screenType, null) as IScreen;
                        break;
                }
                return Shell.ActivateItemAsync(screen, cancellationToken);
            }
        }
    }
}