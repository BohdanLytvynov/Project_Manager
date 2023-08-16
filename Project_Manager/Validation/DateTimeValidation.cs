using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Manager.Validation
{
    class DateTimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (DateTime.TryParse(value.ToString(), out DateTime d))            
                return new ValidationResult(true, "");
            
            else
                return new ValidationResult(false, "Can't parse to DateTime!");
        }
    }
}
