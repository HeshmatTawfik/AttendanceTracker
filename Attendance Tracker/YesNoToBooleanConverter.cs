using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Attendance_Tracker
{
    public class abscenceValueChange : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "1":
                    return "present";
                case "0":
                    return "absence";

                default:
                    return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
           
            return "no";
        }
    }
}
