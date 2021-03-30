using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace CashRegister.WPF.Extensions
{
    public static class LoadViewModelExtensions
    {
        public static void LoadViewModels(this SimpleContainer container, Assembly assembly)
        {
            var viewModels = assembly.GetTypes()
                .Where(x => x.Namespace?.Contains($"ViewModels") == true
                            && x.FullName?.EndsWith("ViewModel") == true)
                .ToList();
            
            foreach (var viewModel in viewModels)
            {
                container.RegisterSingleton(viewModel, viewModel.Name, viewModel);
            }
        }
    }
}