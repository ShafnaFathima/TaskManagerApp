using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManagerApp.DB;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTaskPage : Page
    {
        private static ObservableCollection<TaskModel> _tasks;
        private static ObservableCollection<TaskModel> _initialTasks=new ObservableCollection<TaskModel>();
        static int count = 0;
        public EditTaskPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _tasks = _tasks = TaskDB.GetAssignedTasks(App.CurrentUser);
            count = 0;
            _initialTasks.Clear();
            if (EditTaskPage._tasks.Count != 0)
            {
                NoTasks.Visibility = Visibility.Collapsed;
                EditTaskPage.AddItems();
                TasksList.ItemsSource = _initialTasks;
                TasksList.SelectedIndex = 0;
                TasksList.Visibility = Visibility.Visible;
                EditView.Visibility = Visibility.Visible;
            }
            else
            {
                NoTasks.Visibility = Visibility.Visible;
                TasksList.Visibility = Visibility.Collapsed;
                EditView.Visibility = Visibility.Collapsed;
            }
        }
        
        List<UserModel> Users = UserDB.GetUserList();
        TaskModel task;
        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            task = (TaskModel)TasksList.SelectedItem;
            if (task != null)
            {
                TaskName.Text = task.TaskName;
                int index = Users.FindIndex(user => user.Username.Equals(task.AssignedToUser));
                AssignedToUser.ItemsSource = Users;
                AssignedToUser.DisplayMemberPath = "Username";
                AssignedToUser.SelectedValuePath = "Username";
                AssignedToUser.SelectedIndex = index;
                var enumval = Enum.GetValues(typeof(PriorityTypes)).Cast<PriorityTypes>();
                Priority.ItemsSource = enumval.ToList();
                Priority.SelectedIndex = task.Priority;
                StartDate.Date = task.StartDate;
                EndDate.Date = task.EndDate;
                if (task.Description != null)
                {
                    DescriptionTxt.Text = task.Description;
                }
                if (ActualWidth < 700)
                {
                    TasksList.Visibility = Visibility.Collapsed;
                    BackBtn.Visibility = Visibility.Visible;
                    EditView.Visibility = Visibility.Visible;
                }
                if (ActualWidth >= 700)
                {
                    TasksList.Visibility = Visibility.Visible;
                    EditView.Visibility = Visibility.Visible;
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskName.Text) || (AssignedToUser.SelectedIndex == -1) || (Priority.SelectedIndex == -1)
               || (StartDate.Date == null) || (EndDate.Date == null))
            {
                ErrorTxt.Text = "Enter all the fields!";
            }
            else if (StartDate.Date > EndDate.Date)
            {
                ErrorTxt.Text = "Start Date cannot exceed End date!";
            }
            else
            {
                ErrorTxt.Text = "Successfully Updated!";
                if((DateTimeOffset)StartDate.Date!=task.StartDate)
                {
                    TaskDB.UpdateStartDate(task.TaskId, (DateTimeOffset)StartDate.Date);
                }
                if ((DateTimeOffset)EndDate.Date != task.EndDate)
                {
                    TaskDB.UpdateEndDate(task.TaskId, (DateTimeOffset)EndDate.Date);
                }
                PriorityTypes priority = (PriorityTypes)Enum.Parse(typeof(PriorityTypes), Priority.SelectedItem.ToString());
                if ((int)priority!= task.Priority)
                {
                    TaskDB.UpdatePriority(task.TaskId, (int)priority);
                }
                if(!DescriptionTxt.Text.Equals(task.Description))
                {
                    TaskDB.UpdateDescription(task.TaskId, DescriptionTxt.Text);
                }
                string currAssignedTo= AssignedToUser.SelectedValue.ToString();
                if(!currAssignedTo.Equals(task.AssignedToUser))
                {
                    TaskDB.UpdateAssignedTo(task.TaskId, currAssignedTo);
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            TasksList.Visibility = Visibility.Visible;
            EditView.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Collapsed;
        }

        private void Delete_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
        {
            EditTaskPage._tasks.Remove((TaskModel)args.SwipeControl.DataContext);
            TaskModel swipedTask = (TaskModel)args.SwipeControl.DataContext;
            TaskDB.RemoveTask(swipedTask.TaskId);
        }

        private void TasksListScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                AddItems();
            }
        }
        private static void AddItems()
        {
            int iterator;
            for( iterator=count; iterator<count+15; iterator++ )
            {
                if(iterator<_tasks.Count)
                {
                    _initialTasks.Add(_tasks[iterator]);
                }
                else
                {
                    break;
                }
            }
            count += iterator;
        }
    }
}
