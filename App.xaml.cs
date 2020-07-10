using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.SqlServer;
using SQLite.Net;
using System.Security.Cryptography.X509Certificates;
using TaskManagerApp.DB;
using Windows.Storage;
using System.ServiceModel.Channels;
using TaskManagerApp.Model;

namespace TaskManagerApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        internal static string CurrentUser;
        public static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Object value = App.localSettings.Values["IsFirstLaunch"];
    
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            DBAdapter.InitializeConnection();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {       
            if(localSettings.Values["IsFirstLaunch"] ==null||(bool)value==true)
            {
                
                    localSettings.Values["IsFirstLaunch"] = false;
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    vault.Add(new Windows.Security.Credentials.PasswordCredential("TaskManagerApp", "Sabi", "123"));
                    vault.Add(new Windows.Security.Credentials.PasswordCredential("TaskManagerApp", "Sabitha", "123"));
                    UserDB.AddUser("Sabi", "/Assets/avatar1.PNG");
                    UserDB.AddUser("Sabitha", "/Assets/avatar4.PNG");
                    TaskDB.AddTask(new Model.TaskModel() { TaskName = "Cliq Login Page", TaskId = 12233344441, 
                        AssignedByUser = "Sabi", AssignedToUser = "Sabi", Priority = 1, StartDate = (DateTimeOffset)DateTime.Today,
                        EndDate = (DateTimeOffset)DateTime.Today, Description = "lopoioiui wtyetyuiyruiewo" });
                    TaskDB.AddTask(new Model.TaskModel()
                    {
                        TaskName = "Cliq SignIn Page",
                        TaskId = 9876543211,
                        AssignedByUser = "Sabitha",
                        AssignedToUser = "Sabi",
                        Priority = 2,
                        StartDate = (DateTimeOffset)DateTime.Today,
                        EndDate = (DateTimeOffset)DateTime.Today,
                        Description = "lopoioiui wtyetyuiyruiewo wqeqtwet"
                    });
                    TaskDB.AddTask(new Model.TaskModel()
                    {
                        TaskName = "Content Writing",
                        TaskId = 1234567891,
                        AssignedByUser = "Sabitha",
                        AssignedToUser = "Sabitha",
                        Priority = 1,
                        StartDate = (DateTimeOffset)DateTime.Today,
                        EndDate = (DateTimeOffset)DateTime.Today,
                        Description = "lopoioiui wtyetyuiyruiewo yuwiyeueiytiu"
                    });
                    CommentDB.AddComment(new Model.Comment() { AuthorName = "sabi", CommentToTaskId = 1234567891, Content = " If you cannot find a way to fit supporting evidence in just one or two sentences, use a different example altogether. There are certain topics that require a lot of room for explanation, so be careful not to choose a topic for your essay that will require too much evidence to support.",
                                        CommentId=999886601,Date=DateTime.Now,Heart=0,Happy=0,Sad=0,Like=0 });
                    CommentDB.AddComment(new Model.Comment() { AuthorName = "sabitha", CommentToTaskId = 1234567891, Content = "Okay Done!", CommentId = 999886611, Date = DateTime.Now, Heart = 0, Happy = 0, Sad = 0, Like = 0 });
                    CommentDB.AddComment(new Model.Comment() { AuthorName = "sabi", CommentToTaskId = 9876543211, Content = " Only by examining how you reflect on your qualities can college admissions officers gain an understanding", CommentId = 999886621, Date = DateTime.Now, Heart = 0, Happy = 0, Sad = 0, Like = 0 });
                    CommentDB.AddComment(new Model.Comment() { AuthorName = "sabitha", CommentToTaskId = 12233344441, Content = " Present, support, and introspect.", CommentId = 999886631, Date = DateTime.Now, Heart = 0, Happy = 0, Sad = 0, Like = 0 });            
                
            }
          

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter

                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
