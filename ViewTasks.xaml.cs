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
    public sealed partial class ViewUserTask : Page ,INotifyPropertyChanged
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
            if(tasks.Count!=0)
            {
                TasksList.ItemsSource = tasks;
                TasksList.SelectedItem = tasks[0];
                TaskEmptyTxt.Visibility = Visibility.Collapsed;
            }
            else
            {
                TasksList.Visibility = Visibility.Collapsed;
                TaskEmptyTxt.Text = "No Tasks!";
            }
           
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksList.SelectedItem != null)
            {
                TaskModel task = (TaskModel)TasksList.SelectedItem;

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
                StarBtnDetails.Tag=task.TaskId;
                BoolToColourConverter BoolToColour = new BoolToColourConverter();
                Binding binding = new Binding();
                binding.Converter = BoolToColour;
                binding.Source = StarBtnDetails;
                binding.Path =new PropertyPath("Tag");
                StarBtnDetails.SetBinding(Button.BackgroundProperty, binding);


            } 
        }

        private void StarBtnDetails_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            long taskId = (long)tag;

            bool isFavourite = UserDB.IsFavouriteTask(taskId, App.CurrentUser.ToString());

            if (isFavourite)
            {
                StarBtnDetails.Background = originalBrush;
                UserDB.RemoveFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                OnPropertyChanged("Removed");
            }
            else
            {
                StarBtnDetails.Background = newBrush;
                UserDB.AddFavouriteTaskIds(taskId, App.CurrentUser.ToString());
                OnPropertyChanged("Added");
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

        /* 
                private void AddCommentBtn_Click()
        {   
        var tag = (sender as Button).Tag;
            long taskId = (long)tag;
        CommentModel comment=new CommentModel();
        comment.CommentToTaskId=taskId;
        comment.AuthorName=App.CurrentUser.ToString();
        comment.CommentId=DateTime.Now.Ticks;
        comment.ParentCommentID=0;
        comment.Date=DateTime.Now;
        comment.Content=TextBox.Text;

         */

    }
}
