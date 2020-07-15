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
using TaskManagerApp.DB;
using TaskManagerApp.Views;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp.LoginPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrEmpty(UsernameTxt.Text) == true) || (string.IsNullOrEmpty(PasswordTxt.Password) == true))
            {
                ErrorTxt.Text = "Enter all the fields!";
            }
            else
            {
                bool isValid = UserDB.CheckValidUser(UsernameTxt.Text);
                if (isValid == true)
                {
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    var credential = vault.Retrieve("TaskManagerApp", UsernameTxt.Text);
                    if (credential.Password.Equals(PasswordTxt.Password))
                    {
                        App.CurrentUser = UsernameTxt.Text;
                        App.localSettings.Values["UserLoggedIn"] = App.CurrentUser;
                        this.Frame.Navigate(typeof(HomePage));
                    }
                    else
                    {
                        ErrorTxt.Text = "Incorrect Password!";
                    }
                }
                else
                {
                    ErrorTxt.Text = "User Name does not exist!";
                }
            }
        }
    }
}
