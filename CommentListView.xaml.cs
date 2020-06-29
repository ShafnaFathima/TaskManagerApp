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
using TaskManagerApp.Model;
using TaskManagerApp.DB;
using System.Collections.ObjectModel;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TaskManagerApp
{
    public sealed partial class CommentListView : UserControl
    {
        public Comment ZComment
        {
            get { return ((Comment)GetValue(TaskProperty)); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("ZComment", typeof(Comment), typeof(CommentListView), new PropertyMetadata(null, new PropertyChangedCallback(TaskChanged)));

        public CommentListView()
        {
            this.InitializeComponent();
            
        }

        private static void TaskChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            if (dpo is CommentListView page && page.ZComment != null)
            {
                

            }

        }

        private void RemoveComment_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            long commentId = (long)tag;
            Comment comment = CommentDB.GetComment(commentId);
           // ViewUserTask page = new ViewUserTask();
            

            foreach(Comment comm in ViewUserTask.comments)
            {
                if(comm.CommentId==commentId)
                {
                    ViewUserTask.comments.Remove(comm);
                    break;
                }
            }

            //Console.WriteLine(val);
            CommentDB.RemoveComment(commentId);
           
        }
        //Binding b = new Binding();
        //b=BindingMode.TwoWay;

    }

}
