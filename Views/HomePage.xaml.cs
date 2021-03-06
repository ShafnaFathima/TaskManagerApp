﻿using System;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using TaskManagerApp.LoginPages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            HomeFrame.Navigate(typeof(AddTaskPage));
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
                HomeFrame.Navigate(typeof(AddTaskPage), null, new SuppressNavigationTransitionInfo());
                if(this.ActualWidth < 700)
                {
                    MySplitView.IsPaneOpen = false;
                }
            }
            if (ViewFavTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(ViewMyTaskPage), null, new SuppressNavigationTransitionInfo());
                if (this.ActualWidth < 700)
                {
                    MySplitView.IsPaneOpen = false;
                }
            }
            if (ViewTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(ViewUserTask), null, new SuppressNavigationTransitionInfo());
                if (this.ActualWidth < 700)
                {
                    MySplitView.IsPaneOpen = false;
                }
            }
            if (EditTask.IsSelected)
            {
                HomeFrame.Navigate(typeof(EditTaskPage), null, new SuppressNavigationTransitionInfo());
                if (this.ActualWidth < 700)
                {
                    MySplitView.IsPaneOpen = false;
                }
            }
            else if (Logout.IsSelected)
            {
                App.localSettings.Values["UserLoggedIn"] = null;
                Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}
