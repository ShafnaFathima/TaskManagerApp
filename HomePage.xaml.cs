using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {


        public HomePage()
        {
            this.InitializeComponent();

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = "";
            this.Frame.Navigate(typeof(LoginPage));
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(AddTaskPage));
            }
            if (ViewFavTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(ViewMyTaskPage));

            }
            if (ViewTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(ViewUserTask));

            }
            else if (Logout.IsSelected)
            {
                App.localSettings.Values["UserLoggedIn"] = null;
                Frame.Navigate(typeof(LoginPage));
            }

        }
    }
}
