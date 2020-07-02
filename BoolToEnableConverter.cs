using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using TaskManagerApp.DB;
using Windows.UI.Xaml;

namespace TaskManagerApp
{
    public class BoolToEnableConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // throw new NotImplementedException();
            long commentId = long.Parse(value.ToString());
            bool isReacted = CommentDB.IsReacted(commentId, App.CurrentUser);
            if (isReacted == false)
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
