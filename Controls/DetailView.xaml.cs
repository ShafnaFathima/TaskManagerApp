using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace TaskManagerApp.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page
    {
        public DetailView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TaskModel task = e.Parameter as TaskModel;
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
        }
    }
}

