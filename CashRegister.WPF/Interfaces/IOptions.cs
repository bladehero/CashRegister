namespace CashRegister.WPF.Interfaces
{
    public interface IOptions<out T> where T : class, new()
    {
        public T Value { get; }
    }
}