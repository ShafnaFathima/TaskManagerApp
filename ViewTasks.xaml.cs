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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewUserTask : Page
    {
        SolidColorBrush originalBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush newBrush = new SolidColorBrush(Colors.Yellow);
        public ViewUserTask()
        {
            this.InitializeComponent();

            List<UserModel> users = UserDB.GetUserList();
            SelectUser.ItemsSource = users;
            SelectUser.DisplayMemberPath = "Username";          
        }
        
        private void SelectUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel user = SelectUser.SelectedItem as UserModel;
            checkTxt.Text = user.Username;
            ObservableCollection<TaskModel> tasks = TaskDB.GetTasks(user.Username);
            if(tasks.Count!=0)
            {
                TasksList.ItemsSource = tasks;
            }
            else
            {
                TaskName.Text = "No tasks";
            }
           
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskModel task = TasksList.SelectedItem as TaskModel;
            TaskName.Text = task.TaskName;
            TaskDescription.Text = task.Description;
            TaskId.Text = task.TaskId.ToString();
            string priority = Enum.GetName(typeof(AddTaskPage.PriorityTypes), task.Priority);
            Priority.Text = priority;
            AssignedBy.Text = task.AssignedByUser;
            AssignedTo.Text = task.AssignedToUser;
            Startdate.Text = task.StartDate.ToString();
            EndDate.Text = task.EndDate.ToString();
        }
    }
}
