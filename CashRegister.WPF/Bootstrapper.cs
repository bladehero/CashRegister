using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using CashRegister.Data;
using CashRegister.Interfaces;
using CashRegister.Services;
using CashRegister.WPF.Extensions;
using CashRegister.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CashRegister.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            #region Configuration

            _container.Handler<IConfiguration>(_ => ConfigurationFactory.GetConfiguration());

            #endregion

            #region Database

            _container.Handler<IAppDbContext>(x =>
            {
                var configuration = x.GetInstance<IConfiguration>();
                var connection = configuration.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                var options = optionsBuilder.UseSqlite(connection).Options;
                var context = new AppDbContext(options);
                return context;
            });

            #endregion

            #region DI

            _container.Singleton<IMapperProvider, DomainToServiceMapper>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.PerRequest<IUserStorage, UserStorage>();
            _container.PerRequest<ISessionRegister, SessionRegister>();

            #endregion

            #region ViewModels

            _container.LoadViewModels(Assembly.GetExecutingAssembly());
            _container.RegisterShellProvider();

            #endregion
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}