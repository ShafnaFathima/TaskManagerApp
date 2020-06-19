using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewMyTaskPage : Page
    { 
        public ViewMyTaskPage()
        {
            this.InitializeComponent();
            ObservableCollection<long> favTaskIds = UserDB.GetFavTasks(App.CurrentUser.ToString());
            ObservableCollection<TaskModel> tasks = TaskDB.GetTasksFromId(favTaskIds);
            TasksList.ItemsSource = tasks;
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskModel selectedTask=TasksList.SelectedItem as TaskModel;         
        }        
    }
}
