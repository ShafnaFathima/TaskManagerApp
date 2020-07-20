using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using TaskManagerApp.DB;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace TaskManagerApp.Converters
{   
   
    public class BoolToColourConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isFav = bool.Parse(value.ToString());
            if ( isFav.Equals(false))
            {
                return "\uE734";
            }
            else
            {
                return "\uE735";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
