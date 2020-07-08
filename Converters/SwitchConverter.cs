using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TaskManagerApp.Converters
{

    public class SwitchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string reaction = value.ToString();
            var img = new BitmapImage();
            if (reaction.Equals("happy"))
            {
                img.UriSource = new Uri("ms-appx:///Assets/happy.PNG", UriKind.Absolute);
            }
            else if (reaction.Equals("heart"))
            {
                img.UriSource = new Uri("ms-appx:///Assets/heart.PNG", UriKind.Absolute);
            }
            else if (reaction.Equals("sad"))
            {
                img.UriSource = new Uri("ms-appx:///Assets/sad.PNG", UriKind.Absolute);
            }
            else if(reaction.Equals("like"))
            {

                img.UriSource = new Uri("ms-appx:///Assets/like.PNG", UriKind.Absolute);
            }
            else
            {
                img.UriSource = new Uri("ms-appx:///Assets/likeunfill.PNG", UriKind.Absolute);
            }

            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
