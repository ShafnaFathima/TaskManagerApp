﻿using System;
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
using System.Threading.Tasks;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewUserTask : Page, INotifyPropertyChanged
    {
        SolidColorBrush originalBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush newBrush = new SolidColorBrush(Colors.Yellow);

        public ViewUserTask()
        {
            this.InitializeComponent();

            List<UserModel> users = UserDB.GetUserList();

            int index = users.FindIndex(user => user.Username.Equals(App.CurrentUser.ToString()));
            SelectUser.ItemsSource = users;
            SelectUser.DisplayMemberPath = "Username";
            SelectUser.SelectedIndex = index;
        }

        private void SelectUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel user = (UserModel)SelectUser.SelectedItem;
            ObservableCollection<TaskModel> tasks = TaskDB.GetTasks(user.Username);
            if (tasks.Count != 0)
            {
                TasksList.ItemsSource = tasks;
                TasksList.SelectedIndex = 0;
                TaskEmptyTxt.Visibility = Visibility.Collapsed;
               // StarBtnDetails.Visibility = Visibility.Visible;
                Pic.Visibility = Visibility.Visible;
                PrioritySymbol.Visibility = Visibility.Visible;
                Calendar.Visibility = Visibility.Visible;
                TaskIdsymbol.Visibility = Visibility.Visible;
                Description.Visibility = Visibility.Visible;
                CommentTitle.Visibility = Visibility.Visible;
                Border.Visibility = Visibility.Visible;
                EnterComment.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Visible;
                CommentsList.Visibility = Visibility.Visible;
                TasksList.Visibility = Visibility.Visible;
            }
            else
            {
                TasksList.Visibility = Visibility.Collapsed;
                TaskEmptyTxt.Visibility = Visibility.Visible;
                TaskEmptyTxt.Text = "No Tasks!";
               // StarBtnDetails.Visibility = Visibility.Collapsed;
                Pic.Visibility = Visibility.Collapsed;
                PrioritySymbol.Visibility = Visibility.Collapsed;
                Calendar.Visibility = Visibility.Collapsed;
                TaskIdsymbol.Visibility = Visibility.Collapsed;
                Description.Visibility = Visibility.Collapsed;
                CommentTitle.Visibility = Visibility.Collapsed;
                Border.Visibility = Visibility.Collapsed;
                EnterComment.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Collapsed;
                CommentsList.Visibility = Visibility.Collapsed;
            }
        }
        public static ObservableCollection<Comment> comments;
        public static FavoriteTask fav;
        /// List<Comment> comments = new List<Comment>();
        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             TaskModel task = (TaskModel)TasksList.SelectedItem;
            if (task != null)
            {
                TitleTxt.Text = task.TaskName;
                DescriptionTxt.Text = task.Description;
                TaskIdTxt.Text = task.TaskId.ToString();
                string priority = Enum.GetName(typeof(PriorityTypes), task.Priority);
                PriorityTxt.Text = priority;
                AssignedByTxt.Text = task.AssignedByUser;
                string fmt = "d";
                string Startdate = task.StartDate.Date.ToString(fmt);
                string EndDate = task.EndDate.Date.ToString(fmt);
                DateTxt.Text = Startdate + " to " + EndDate;
                bool IsAlreadyFav = UserDB.IsFavouriteTask(task.TaskId, App.CurrentUser);
                if(!IsAlreadyFav)
                {
                    UserDB.AddFavouriteTaskIds(task.TaskId, App.CurrentUser);
                }
                fav = UserDB.GetFavorite(task.TaskId, App.CurrentUser);
                StarBtnDetails.DataContext = fav;
                comments = CommentDB.GetComments(task.TaskId);
               // Btn.ItemsSource = fav;
                CommentsList.ItemsSource = comments;
                AddButton.Tag = task.TaskId;
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if(fav.IsFavourite==true)
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
    }
}
