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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTaskPage : Page
    {
        //private List<string> _priorityTypes = new List<string>() {"Low","Medium","High"};

        public enum PriorityTypes
        {
            Low,
            Medium,
            High
        }

        public AddTaskPage()
        {
            this.InitializeComponent();
            List<UserModel> Users = UserDB.GetUserList();
            AssignedToUser.ItemsSource = Users;
            AssignedToUser.DisplayMemberPath = "Username";
            AssignedToUser.SelectedValuePath = "Username";

            var _enumval = Enum.GetValues(typeof(PriorityTypes)).Cast<PriorityTypes>();
            Priority.ItemsSource = _enumval.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskName.Text) || (AssignedToUser.SelectedIndex == -1)||(Priority.SelectedIndex==-1)
                ||string.IsNullOrEmpty(DescriptionTxt.Text)||(StartDate.Date==null)||(EndDate.Date==null))
            {
                ErrorTxt.Text = "Enter all the fields!";
            }
            else
            {
                TaskModel Task = new TaskModel();
                Task.AssignedToUser = AssignedToUser.SelectedValue.ToString();
                Task.AssignedByUser = App.CurrentUser;
                Task.TaskName = TaskName.Text;
                PriorityTypes priority = (PriorityTypes)Enum.Parse(typeof(PriorityTypes),Priority.SelectedItem.ToString());
                Task.Priority = (int)priority;
                Task.StartDate =(DateTimeOffset)StartDate.Date;
                Task.EndDate = (DateTimeOffset)EndDate.Date;
                Task.Description = DescriptionTxt.Text;
                TaskDB.AddTask(Task);
                ErrorTxt.Text = "Successfully Added!";
            }
        }
    }
}
