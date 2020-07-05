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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManagerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
            //List<Image> avatars = new List<Image>();
            
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrEmpty(UsernameTxt.Text) == true) || (string.IsNullOrEmpty(PasswordTxt.Text) == true) || (string.IsNullOrEmpty(ConfirmPasswordTxt.Text) == true))
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
                    if (PasswordTxt.Text.Equals(ConfirmPasswordTxt.Text))
                    {
                        UserDB.AddUser(UsernameTxt.Text);
                        var vault = new Windows.Security.Credentials.PasswordVault();
                        vault.Add(new Windows.Security.Credentials.PasswordCredential("TaskManagerApp", UsernameTxt.Text, PasswordTxt.Text));
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
