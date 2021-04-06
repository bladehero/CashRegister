using System.Windows;
using System.Windows.Media;

namespace CashRegister.WPF.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {    
            if (parent == null) return null;

            T foundChild = null;
            var isNameEmpty = string.IsNullOrWhiteSpace(childName);

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (isNameEmpty == false && (child as FrameworkElement)?.Name == childName)
                {
                    foundChild = (T)child;
                    break;
                }

                if (foundChild is not null) break;
                
                foundChild = FindChild<T>(child, childName);
            }

            return foundChild;
        }
    }
}