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
    public class ValidateMoreThanZero : ValidationRule
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
            if (norm < 0)
            {
                return new ValidationResult(false, "Значение должно быть больше нуля");
            }
            return new ValidationResult(true, null);
        }
    }
}
