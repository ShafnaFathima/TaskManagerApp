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
using Windows.UI;
using TaskManagerApp.DB;
using System.Globalization;
using System.ComponentModel;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TaskManagerApp
{
    public sealed partial class ListViewUserControl : UserControl //,INotifyPropertyChanged
    {
        SolidColorBrush originalBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush newBrush = new SolidColorBrush(Colors.Yellow);

        public TaskModel ZTask
        {
            get {
                this.OnLoaded((TaskModel)GetValue(TaskProperty));
                return (TaskModel)GetValue(TaskProperty);
                
            }
            set { SetValue(TaskProperty, value);  

            }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("ZTask", typeof(TaskModel), typeof(ListViewUserControl), new PropertyMetadata(null, new PropertyChangedCallback(TaskChanged)));

      
        private static void TaskChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            if (dpo is ListViewUserControl page && page.ZTask != null)
            {


            }
        }
        public ListViewUserControl()
        {
            this.InitializeComponent();
            

        }
        FavoriteTask favorite;
        public void OnLoaded(TaskModel ZTask)
        {
            bool IsAlreadyFav = UserDB.IsFavouriteTask(ZTask.TaskId, App.CurrentUser);
            if (!IsAlreadyFav)
            {
                UserDB.AddFavouriteTaskIds(ZTask.TaskId, App.CurrentUser);
            }
            favorite = UserDB.GetFavorite(ZTask.TaskId, App.CurrentUser);
           this.StarBtn.DataContext = favorite;
        }

        //  public Event Action<string,bool> FavouriteClick;
        public event Action<long, bool> StatusChanged;

      /* public class ProcessEventArgs : EventArgs
        {
            public bool IsFav { get; set; }
            public FavoriteTask FavTask { get; set; }
        }*/

        private void StarBtn_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            long taskId = (long)tag;
            bool IsAlreadyFav = UserDB.IsFavouriteTask(taskId, App.CurrentUser);
           // var task = new ProcessEventArgs();
            if (!IsAlreadyFav)
            {
                UserDB.AddFavouriteTaskIds(taskId, App.CurrentUser);
            }
            if (this.favorite.IsFavourite == true)
            {
                this.favorite.IsFavourite = false;
                UserDB.RemoveFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                
                OnClick(taskId, false);
            }
            else
            {
                this.favorite.IsFavourite = true;
                UserDB.AddFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                OnClick(taskId, true);
               
            }
        }
        protected  void OnClick(long taskId,bool isFav)
        {
            StatusChanged?.Invoke(taskId,isFav);
        }
    }
}
