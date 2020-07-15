using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManagerApp.DB;
using TaskManagerApp.Controls;
using TaskManagerApp.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewMyTaskPage : Page
    {
        private static ObservableCollection<TaskModel> _tasks;
        ObservableCollection<long> _favTaskIds;
        public ViewMyTaskPage()
        {
            this.InitializeComponent();
            _favTaskIds= UserDB.GetFavTasks(App.CurrentUser.ToString());
            _tasks = _tasks=TaskDB.GetTasksFromId(_favTaskIds);
            this.OnLoaded();
        }

        public void OnLoaded()
        {
            if (ViewMyTaskPage._tasks.Count != 0)
            {
                NoTasks.Visibility = Visibility.Collapsed;
                TasksList.ItemsSource = _tasks;
                TasksList.SelectedIndex = 0;
                TasksList.Visibility = Visibility.Visible;
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
            }
            else
            {
                NoTasks.Visibility = Visibility.Visible;
                TasksList.Visibility = Visibility.Collapsed;
                DetailsGrid.Visibility = Visibility.Collapsed;
                Discussion.Visibility = Visibility.Collapsed;
            }
        }
        public static ObservableCollection<Comment> comments;
        public static FavoriteTask fav;
        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskModel task=TasksList.SelectedItem as TaskModel;
            if (task != null)
            {
                TitleTxt.Text = task.TaskName;
                if (string.IsNullOrEmpty(task.Description))
                {
                    DescriptionPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    DescriptionPanel.Visibility = Visibility.Visible;
                    DescriptionTxt.Text = task.Description;
                }
                string priority = Enum.GetName(typeof(PriorityTypes), task.Priority);
                PriorityTxt.Text = priority;
                AssignedTo.Text = task.AssignedToUser;
                string fmt = "d";
                Assigned.Text = "Assigned to " + task.AssignedToUser + " | Starts on " + task.StartDate.Date.ToString(fmt); ;
                string EndDate = task.EndDate.Date.ToString(fmt);
                DateTxt.Text = EndDate;
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
            }
            double Acutalwidth = this.ActualWidth;
            if(ActualWidth<700)
            {
                TasksList.Visibility = Visibility.Collapsed;
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
            }
            if (ActualWidth >= 700)
            {
                TasksList.Visibility = Visibility.Visible;
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
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
                    foreach(TaskModel favtask in ViewMyTaskPage._tasks)
                    {
                        if(favtask.TaskId==taskId)
                        {
                            ViewMyTaskPage._tasks.Remove(favtask);
                            this.TasksList.ItemsSource = _tasks;
                            this.OnLoaded();
                            break;
                        }
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {   
           
            TasksList.Visibility = Visibility.Visible;
            DetailsGrid.Visibility = Visibility.Collapsed;
            Discussion.Visibility = Visibility.Collapsed;
           // TasksList.SelectedIndex = -1;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ViewMyTaskPage._tasks.Count != 0)
            {
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
            }
            double Acutalwidth = this.ActualWidth;
            if(ActualWidth>=700)
            {
                TasksList.Visibility = Visibility.Visible;
            }
        }

        private void TasksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaskModel task = TasksList.SelectedItem as TaskModel;
            if (task != null)
            {
                TitleTxt.Text = task.TaskName;
                DescriptionTxt.Text = task.Description;
                //TaskIdTxt.Text = task.TaskId.ToString();
                string priority = Enum.GetName(typeof(PriorityTypes), task.Priority);
                PriorityTxt.Text = priority;
                AssignedTo.Text = task.AssignedToUser;
                string fmt = "d";
                Assigned.Text = "Assigned to " + task.AssignedToUser + " | Starts on " + task.StartDate.Date.ToString(fmt); ;
                string EndDate = task.EndDate.Date.ToString(fmt);
                DateTxt.Text = EndDate;
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
            }

            if (this.ActualWidth < 700)
            {
                TasksList.Visibility = Visibility.Collapsed;
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
            }
            if (this.ActualWidth >= 700)
            {
                TasksList.Visibility = Visibility.Visible;
                DetailsGrid.Visibility = Visibility.Visible;
                Discussion.Visibility = Visibility.Visible;
            }
        }
        private void Delete_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            ViewMyTaskPage._tasks.Remove((TaskModel)args.SwipeControl.DataContext);
            TaskModel swipedTask = (TaskModel)args.SwipeControl.DataContext;
            TaskDB.RemoveTask(swipedTask.TaskId);
        }
    }
}
