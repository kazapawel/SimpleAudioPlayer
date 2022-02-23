using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AudioPlayerMVVMandNAudio
{
    class TagToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value=="volume" 
                ? "pack://application:,,,/Images/Volume/audio unmute.png"
                : "pack://application:,,,/Images/Transport/play.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != "pack://application:,,,/Images/Transport/play.png";
        }
    }
}
