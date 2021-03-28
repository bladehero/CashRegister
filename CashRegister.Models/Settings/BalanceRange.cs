namespace CashRegister.Models.Settings
{
    public class BalanceRange
    {
        public decimal Start { get; set; }
        public decimal End { get; set; }

        public bool IsIncluded(decimal value)
        {
            return Start <= value && value <= End;
        }

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }
}