using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AudioPlayerMVVMandNAudio
{
    class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "pack://application:,,,/Images/Transport/pausebutton.png" : "pack://application:,,,/Images/Transport/play.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != "pack://application:,,,/Images/Transport/play.png";
        }
    }
}
