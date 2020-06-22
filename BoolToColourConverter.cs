using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using TaskManagerApp.DB;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace TaskManagerApp
{   
   
    public class BoolToColourConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            long taskId = long.Parse(value.ToString());
            bool IsFav = UserDB.IsFavouriteTask(taskId, App.CurrentUser);
            if (IsFav == false)
            {
                return new SolidColorBrush(Colors.White);
            }
            else
            {

                return new SolidColorBrush(Colors.Yellow);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
