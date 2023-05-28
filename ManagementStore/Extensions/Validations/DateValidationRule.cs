using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ManagementStore.Extensions.Validations
{
    public class DateValidationRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            string input = value?.ToString().Replace("-", ""); // Remove existing hyphens

            if (input?.Length == 8)
            {
                // Parse the input as a date
                if (DateTime.TryParseExact(input, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    int day = parsedDate.Day;
                    int month = parsedDate.Month;
                    int year = parsedDate.Year;

                    if (day < 1 || day > DateTime.DaysInMonth(year, month))
                    {
                        ErrorText = "Invalid day entered.";
                        return false;
                    }
                }
                else
                {
                    ErrorText = "Invalid date format entered.";
                    return false;
                }
            }
            else
            {
                ErrorText = "Invalid date format entered.";
                return false;
            }

            return true;
        }
    }
}
