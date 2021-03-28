using System.Globalization;
using System.Windows.Controls;
using CashRegister.Models.Settings;
using CashRegister.WPF.Extensions;

namespace CashRegister.WPF.Validation.Rules
{
    public class IsDecimalValidationRule : ValidationRule
    {
        private readonly BalanceRange _balanceRange;
        
        public IsDecimalValidationRule()
        {
            _balanceRange = ConfigurationFactory.GetConfiguration().GetRegisterSettings<BalanceRange>();
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
            {
                return new ValidationResult(false, "Field is required.");
            }

            var @string = value.ToString();

            var success = decimal.TryParse(@string, out var @decimal);
            if (success == false)
            {
                return new ValidationResult(false, "Field should be valid decimal.");
            }

            return _balanceRange.IsIncluded(@decimal)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, $"Field should be in range {_balanceRange}.");
        }
    }
}