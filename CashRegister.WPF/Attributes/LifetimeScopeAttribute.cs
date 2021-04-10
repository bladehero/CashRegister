using System;

namespace CashRegister.WPF.Attributes
{
    public enum LifetimeScope
    {
        Scoped,
        Singletone
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LifetimeScopeAttribute : Attribute
    {
        public LifetimeScope LifetimeScope { get; set; } = LifetimeScope.Scoped;

        public LifetimeScopeAttribute()
        {
            
        }

        public LifetimeScopeAttribute(LifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }
    }
}