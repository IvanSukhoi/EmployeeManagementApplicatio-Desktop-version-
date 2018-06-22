using System.Globalization;
using System.Windows.Controls;

namespace EmployeeManagement.UI.Validation
{
    public class NameValidationRule : ValidationRule
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string) value))
            {
                return new ValidationResult(false,
                    "Please enter data");
            }

            int length = ((string) value).Length;


            if ((length < MinLength) || (length > MaxLength))
            {
                return new ValidationResult(false,
                    "Please enter data in the range: " + MinLength + " - " + MaxLength + ".");
            }

            return ValidationResult.ValidResult;
        }
    }
}
