using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Security;
using TaskManagerApp.DB;
using TaskManagerApp.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Security.Authentication.OnlineId;
using Windows.UI;
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
    public sealed partial class AddTaskPage : Page
    {
        List<UserModel> Users = UserDB.GetUserList();
        public AddTaskPage()
        {
            this.InitializeComponent();
            int index = Users.FindIndex(user => user.Username.Equals(App.CurrentUser.ToString()));
            AssignedToUser.ItemsSource = Users;
            AssignedToUser.DisplayMemberPath = "Username";
            AssignedToUser.SelectedValuePath = "Username";
            AssignedToUser.SelectedIndex = index;
            var enumval = Enum.GetValues(typeof(PriorityTypes)).Cast<PriorityTypes>();
            Priority.ItemsSource = enumval.ToList();
            Priority.SelectedIndex = 1;
            StartDate.Date = DateTime.Today;
            EndDate.Date = DateTime.Today;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskName.Text) || (AssignedToUser.SelectedIndex == -1)||(Priority.SelectedIndex==-1)
                ||(StartDate.Date==null)||(EndDate.Date==null))
            {
                ErrorTxt.Text = "Enter all the fields!";
            }
            else if(StartDate.Date>EndDate.Date)
            {
                ErrorTxt.Text = "Start Date cannot exceed End date!";
            }
            else
            {
               
                TaskModel task = new TaskModel();
                task.AssignedToUser = AssignedToUser.SelectedValue.ToString();
                task.AssignedByUser = App.CurrentUser;
                task.TaskName = TaskName.Text;
                PriorityTypes priority = (PriorityTypes)Enum.Parse(typeof(PriorityTypes),Priority.SelectedItem.ToString());
                task.Priority = (int)priority;
                task.StartDate =(DateTimeOffset)StartDate.Date;
                task.EndDate = (DateTimeOffset)EndDate.Date;
                task.Description = DescriptionTxt.Text;
                task.TaskId = DateTime.Now.Ticks;
                TaskDB.AddTask(task);
                ErrorTxt.Text = "Successfully Added!";
                int index = Users.FindIndex(user => user.Username.Equals(App.CurrentUser.ToString()));
                AssignedToUser.ItemsSource = Users;
                AssignedToUser.DisplayMemberPath = "Username";
                AssignedToUser.SelectedValuePath = "Username";
                AssignedToUser.SelectedIndex = index;
                var enumval = Enum.GetValues(typeof(PriorityTypes)).Cast<PriorityTypes>();
                Priority.ItemsSource = enumval.ToList();
                Priority.SelectedIndex = 1;
                StartDate.Date = DateTime.Today;
                EndDate.Date = DateTime.Today;
                TaskName.Text = "";
                DescriptionTxt.Text = "";
            }
        }
    }
}
