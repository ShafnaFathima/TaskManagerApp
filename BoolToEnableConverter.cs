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
            string btnName = parameter.ToString();
            long commentId = long.Parse(value.ToString());
            bool isReacted = CommentDB.IsReacted(commentId, App.CurrentUser);
            if (isReacted == false)
            {
                return true;
            }
            else
            {
                string currentReaction = CommentDB.GetReaction(commentId, App.CurrentUser);
                {
                    if (string.IsNullOrEmpty(currentReaction))
                    {
                        return true;
                    }
                    else if (btnName.Equals("HappyBtn") && currentReaction.Equals("happy"))
                    {
                        return true;
                    }
                    else if (btnName.Equals("HeartBtn") && currentReaction.Equals("heart"))
                    {
                        return true;
                    }
                    else if (btnName.Equals("SadBtn") && currentReaction.Equals("sad"))
                    {
                        return true;
                    }
                    else if (btnName.Equals("LikeBtn") && currentReaction.Equals("like"))
                    {
                        return true;
                    }
                    return false;
                }


            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
