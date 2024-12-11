using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class ImageBorderClass : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 &&
          values[0] is double width &&
          values[1] is double height &&
          width > 0 && height > 0) // Ensure non-zero dimensions
            {
                return new Rect(0, 0, width, height);
            }

            // Fallback to default rectangle
            return new Rect(0, 0, 100, 100); // Use default size for debugging
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
