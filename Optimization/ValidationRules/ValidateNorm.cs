using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Optimization
{
    public class ValidateNorm : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Введите число");
            }
            if (!double.TryParse((string)value, NumberStyles.Float, CultureInfo.CurrentCulture, out var norm))
            {
                return new ValidationResult(false, "Введите число");
            }
            if (norm < 0 || norm > 1)
            {
                return new ValidationResult(false, "Нормирующие множители находятся в диапазоне [0,1]");
            }
            return new ValidationResult(true, null);

        }
    }
}
