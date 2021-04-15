using Client.Converters.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Converters
{
    public class ListStringToStringConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";
            if(value != null)
            {
                var collection = (IList<string>)value;
                foreach(var item in collection)
                {
                    result += $"{item}\r\n";
                }
            }
            return result.Trim('\r','\n');
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
