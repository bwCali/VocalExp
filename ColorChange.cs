using Microsoft.Maui.Graphics.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoMo4
{
    public class ColorChange : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
        ColorTypeConverter converter = new();
            var result = converter.ConvertFromInvariantString((string)value);
            return result ?? Colors.Yellow;
                }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }  
          public object ProvideValue(IServiceProvider serviceProvider)
        { return this; }

        

    }
}
