using Client.Configuration;
using Client.Converters.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Client.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class ServerAvailableConverter : ConverterBase
    {

        public IAppConfig AppConfig { get; }

        public ServerAvailableConverter()
        {
            AppConfig = ResolveType<IAppConfig>();
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;
            return new SolidColorBrush(enabled ? GetColor(AppConfig.ColorConfig.ConnectionEnableColor) : GetColor(AppConfig.ColorConfig.ConnectionDisabled));
        }

        private Color GetColor(string color)
        {
            return (Color)ColorConverter.ConvertFromString(color);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
