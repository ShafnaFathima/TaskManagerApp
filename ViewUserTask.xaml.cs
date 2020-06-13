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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewUserTask : Page
    {
        public ViewUserTask()
        {
            this.InitializeComponent();

            List<UserModel> users = UserDB.GetUserList();
            foreach (UserModel user in users.ToList())
            {
                if (user.Username.Equals(App.CurrentUser))
                {
                    users.Remove(user);
                }
            }

            SelectUser.ItemsSource = users;
            SelectUser.DisplayMemberPath = "Username";
        }

        private void SelectUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel user = SelectUser.SelectedItem as UserModel;
            ObservableCollection<TaskModel> tasks = TaskDB.GetTasks(user.Username);
            TasksList.ItemsSource = tasks;
        }
    }
}
