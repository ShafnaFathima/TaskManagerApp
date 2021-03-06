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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TaskManagerApp.DB;
using TaskManagerApp.Model;
using TaskManagerApp;
using TaskManagerApp.LoginPages;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.LoginPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        private List<string> Icons = new List<string>();
        public SignUpPage()
        {
            this.InitializeComponent();     
            Icons.Add("/Assets/avatar1.PNG");
            Icons.Add("/Assets/avatar2.PNG");
            Icons.Add("/Assets/avatar3.PNG");
            Icons.Add("/Assets/avatar4.PNG");
            AvatarComboBox.ItemsSource = Icons;
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrEmpty(UsernameTxt.Text) == true) || (string.IsNullOrEmpty(PasswordTxt.Password) == true) || (string.IsNullOrEmpty(ConfirmPasswordTxt.Password) == true) ||(AvatarComboBox.SelectedIndex==-1))
            {
                ErrorTxt.Text = "Enter all the fields!";
            }
            else
            {
                string userName = UsernameTxt.Text;
                bool isValid = UserDB.CheckValidUser(UsernameTxt.Text);
                if (isValid == true)
                {
                    ErrorTxt.Text = "User Name already exists!";
                }
                else
                {
                    if (PasswordTxt.Password.Equals(ConfirmPasswordTxt.Password))
                    {
                        string avatar = ((string)AvatarComboBox.SelectedValue);
                        AvatarComboBox.SelectedIndex = -1;
                        UserDB.AddUser(UsernameTxt.Text,avatar);
                        var vault = new Windows.Security.Credentials.PasswordVault();
                        vault.Add(new Windows.Security.Credentials.PasswordCredential("TaskManagerApp", UsernameTxt.Text, PasswordTxt.Password));
                        Frame.Navigate(typeof(LoginPage));
                    }
                    else
                    {
                        ErrorTxt.Text = "Passwords don't match!";
                    }
                }
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
