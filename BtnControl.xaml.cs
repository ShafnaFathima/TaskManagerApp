using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TaskManagerApp.Model;
using TaskManagerApp.DB;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TaskManagerApp
{
    public sealed partial class BtnControl : UserControl
    {
        public FavoriteTask ZFav
        {
            get { return ((FavoriteTask)GetValue(TaskProperty)); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("ZFav", typeof(Comment), typeof(BtnControl), new PropertyMetadata(null, new PropertyChangedCallback(TaskChanged)));

        private static void TaskChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            if (dpo is BtnControl page && page.ZFav != null)
            {


            }

        }

        public BtnControl()
        {
            this.InitializeComponent();
        }
        private void StarBtn_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            long taskId = (long)tag;

            bool isFavourite = UserDB.IsFavouriteTask(taskId, App.CurrentUser.ToString());

            if (isFavourite)
            {
                //StarBtnDetails.Background = originalBrush;
                UserDB.RemoveFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                ZFav.IsFavourite = false;

            }
            else
            {
              //  StarBtnDetails.Background = newBrush;
                UserDB.AddFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                ZFav.IsFavourite = true;
            }
        }
    }
}
