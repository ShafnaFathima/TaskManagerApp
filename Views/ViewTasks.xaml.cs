using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TaskManagerApp.DB;
using TaskManagerApp.Model;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Globalization;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using System.ComponentModel;
using TaskManagerApp.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewUserTask : Page, INotifyPropertyChanged
    {


        //public ViewUserTask()
        //{
        //    this.InitializeComponent();
        //    //this._tasks = TaskDB.GetTasks(App.CurrentUser.ToString());
        //    // this.OnLoaded();
        //}
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    this._tasks = TaskDB.GetTasks(App.CurrentUser.ToString()); 
        //     List<UserModel> users = UserDB.GetUserList();
        //      int index = users.FindIndex(user => user.Username.Equals(App.CurrentUser.ToString()));
        //      SelectUser.ItemsSource = users;
        //      SelectUser.DisplayMemberPath = "Username";
        //     SelectUser.SelectedIndex = index;
        //}

       
        private static ObservableCollection<TaskModel> _tasks;
        
        public ViewUserTask()
        {
            this.InitializeComponent();
           
          
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _tasks = TaskDB.GetTasks(App.CurrentUser.ToString());
            List<UserModel> users = UserDB.GetUserList();
            int index = users.FindIndex(user => user.Username.Equals(App.CurrentUser.ToString()));
            SelectUser.ItemsSource = users;
            SelectUser.DisplayMemberPath = "Username";
            SelectUser.SelectedIndex = index;
            if (ViewUserTask._tasks.Count != 0)
            {
                NoTasks.Visibility = Visibility.Collapsed;
                TasksList.ItemsSource = _tasks;
                TasksList.SelectedIndex = 0;
                TasksList.Visibility = Visibility.Visible;
                DetailAndDiscussion.Visibility = Visibility.Visible;
            }
            else
            {
                NoTasks.Visibility = Visibility.Visible;
                TasksList.Visibility = Visibility.Collapsed;
                DetailAndDiscussion.Visibility = Visibility.Collapsed;
            }
        }
        public static ObservableCollection<Comment> comments;
        public static FavoriteTask fav;

        public event PropertyChangedEventHandler PropertyChanged;
        private void SelectUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel user = (UserModel)SelectUser.SelectedItem;
            ViewUserTask._tasks = TaskDB.GetTasks(user.Username);
            if (ViewUserTask._tasks != null)
            {
                if (ViewUserTask._tasks.Count != 0)
                {
                    TasksList.ItemsSource = ViewUserTask._tasks;
                    TasksList.SelectedIndex = 0;
                    NoTasks.Visibility = Visibility.Collapsed;
                   /* double Acutalwidth = this.ActualWidth;
                    if (ActualWidth < 700)
                    {
                        TasksList.Visibility = Visibility.Visible;
                        ComboboxPanel.Visibility = Visibility.Visible;
                        DetailAndDiscussion.Visibility = Visibility.Collapsed;
                        BackBtn.Visibility = Visibility.Collapsed;
                    }
                    if (ActualWidth >= 700)
                    {
                        TasksList.Visibility = Visibility.Visible;
                        DetailAndDiscussion.Visibility = Visibility.Visible;
                    }*/
                }
                else
                {
                    //TasksList.ItemsSource = _tasks;
                    NoTasks.Visibility = Visibility.Visible;
                    TasksList.Visibility = Visibility.Collapsed;
                    DetailAndDiscussion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskModel task = TasksList.SelectedItem as TaskModel;
            if (task != null)
            {
                TitleTxt.Text = task.TaskName;
                bool IsAlreadyFav = UserDB.IsFavouriteTask(task.TaskId, App.CurrentUser);
                if (!IsAlreadyFav)
                {
                    UserDB.AddFavouriteTaskIds(task.TaskId, App.CurrentUser);
                }
                fav = UserDB.GetFavorite(task.TaskId, App.CurrentUser);
                StarBtnDetails.DataContext = fav;
                comments = CommentDB.GetComments(task.TaskId);
                CommentsList.ItemsSource = comments;
                AddButton.Tag = task.TaskId;
                DetailsFrame.Navigate(typeof(DetailView), task, new SuppressNavigationTransitionInfo());
            }
            double Acutalwidth = this.ActualWidth;
            if (ActualWidth < 700)
            {
                TasksList.Visibility = Visibility.Collapsed;
                ComboboxPanel.Visibility = Visibility.Collapsed;
                DetailAndDiscussion.Visibility = Visibility.Visible;
                BackBtn.Visibility = Visibility.Visible;
            }
            if (ActualWidth >= 700)
            {
                TasksList.Visibility = Visibility.Visible;
                DetailAndDiscussion.Visibility = Visibility.Visible;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EnterComment.Text))
            {
                EnterComment.PlaceholderText = "Empty comment!";
                EnterComment.PlaceholderForeground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                EnterComment.PlaceholderForeground = new SolidColorBrush(Colors.Gray);
                EnterComment.PlaceholderText = "Write your comment here!";
                var tag = (sender as Button).Tag;
                long taskId = (long)tag;
                Comment comment = new Comment();
                comment.CommentToTaskId = taskId;
                comment.AuthorName = App.CurrentUser;
                comment.Content = EnterComment.Text;
                comment.CommentId = DateTime.Now.Ticks;
                comment.Date = DateTime.Now;
                CommentDB.AddComment(comment);
                comments.Add(comment);
                //comments = CommentDB.GetComments(taskId);
                CommentsList.ItemsSource = comments;
                EnterComment.Text = "";
            }
        }
        private void StarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fav.IsFavourite == true)
            {
                fav.IsFavourite = false;
                UserDB.RemoveFavouriteTaskIds(fav.TaskId, App.CurrentUser.ToString());
            }
            else
            {
                fav.IsFavourite = true;
                UserDB.AddFavouriteTaskIds(fav.TaskId, App.CurrentUser.ToString());
            }
        }
        public void list_FavChanged(long taskId, bool isFav)
        {
            //var favobj = e.FavTask;
            if (taskId == fav.TaskId)
            {
                if (isFav == true)
                {
                    fav.IsFavourite = true;

                }
                else if (isFav == false)
                {
                    fav.IsFavourite = false;
                    foreach (TaskModel favtask in ViewUserTask._tasks)
                    {
                        if (favtask.TaskId == taskId)
                        {
                            ViewUserTask._tasks.Remove(favtask);
                            this.TasksList.ItemsSource = _tasks;
                            //this.OnLoaded();
                            break;
                        }
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            TasksList.Visibility = Visibility.Visible;
            ComboboxPanel.Visibility = Visibility.Visible;
            DetailAndDiscussion.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Collapsed;
           
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 700)
            {
                if (ViewUserTask._tasks.Count != 0)
                {
                    if (TasksList.Visibility == Visibility.Visible)
                    {
                        DetailAndDiscussion.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DetailAndDiscussion.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    DetailAndDiscussion.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (ViewUserTask._tasks.Count != 0)
                {
                    ComboboxPanel.Visibility = Visibility.Visible;
                    TasksList.Visibility = Visibility.Visible;
                    DetailAndDiscussion.Visibility = Visibility.Visible;
                }
                else
                {
                    ComboboxPanel.Visibility = Visibility.Visible;
                    TasksList.Visibility = Visibility.Collapsed;
                    DetailAndDiscussion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TasksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaskModel task = TasksList.SelectedItem as TaskModel;
            if (task != null)
            {
                TitleTxt.Text = task.TaskName;
                bool IsAlreadyFav = UserDB.IsFavouriteTask(task.TaskId, App.CurrentUser);
                if (!IsAlreadyFav)
                {
                    UserDB.AddFavouriteTaskIds(task.TaskId, App.CurrentUser);
                }
                fav = UserDB.GetFavorite(task.TaskId, App.CurrentUser);
                StarBtnDetails.DataContext = fav;
                comments = CommentDB.GetComments(task.TaskId);
                CommentsList.ItemsSource = comments;
                AddButton.Tag = task.TaskId;
                DetailsFrame.Navigate(typeof(DetailView), task, new SuppressNavigationTransitionInfo());
            }

            if (this.ActualWidth < 700)
            {


                TasksList.Visibility = Visibility.Visible;
                DetailAndDiscussion.Visibility = Visibility.Collapsed;


                TasksList.Visibility = Visibility.Collapsed;
                DetailAndDiscussion.Visibility = Visibility.Visible;

            }
            if (this.ActualWidth >= 700)
            {
                TasksList.Visibility = Visibility.Visible;
                DetailAndDiscussion.Visibility = Visibility.Visible;
            }
        }
        private void Delete_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            ViewUserTask._tasks.Remove((TaskModel)args.SwipeControl.DataContext);
            TaskModel swipedTask = (TaskModel)args.SwipeControl.DataContext;
            TaskDB.RemoveTask(swipedTask.TaskId);
        }



    }
}
