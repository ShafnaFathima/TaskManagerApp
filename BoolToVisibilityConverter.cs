using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using TaskManagerApp.DB;
using Windows.UI;
using Windows.UI.Xaml;

namespace TaskManagerApp
{
    public class BoolToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // throw new NotImplementedException();
            long commentId = long.Parse(value.ToString());
            //bool IsFav = UserDB.IsFavouriteTask(taskId, App.CurrentUser);
            bool isMyComment = CommentDB.IsMyComment(commentId, App.CurrentUser);
            if (isMyComment == false)
            {
                return Visibility.Collapsed;
            }
            else
            {

                return Visibility.Visible ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
