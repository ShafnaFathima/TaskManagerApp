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
using System.Security.Cryptography.X509Certificates;
using Windows.System;
using System.Runtime.CompilerServices;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TaskManagerApp
{
    public sealed partial class CommentListView : UserControl
    {
        public Comment ZComment
        {          
            get {
                if ((Comment)GetValue(TaskProperty) != null)
                {
                    OnLoaded((Comment)GetValue(TaskProperty));
                }
                return ((Comment)GetValue(TaskProperty)); }
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
        public  void OnLoaded(Comment comment)
        {
            UserModel commentUser = UserDB.GetUser(comment.AuthorName);
            this.AvatarPic.DataContext = commentUser;
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
            CommentDB.RemoveComment(commentId);         
        }
        private void HeartBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Heart += 1;
                HeartBtn.IsEnabled = true;
                SadBtn.IsEnabled = false;
                LikeBtn.IsEnabled = false;
                HappyBtn.IsEnabled = false;
                CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "heart");
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (string.IsNullOrEmpty(currentReaction))
                {
                    ZComment.Heart+= 1;
                    HeartBtn.IsEnabled = true;
                    SadBtn.IsEnabled = false;
                    LikeBtn.IsEnabled = false;
                    HappyBtn.IsEnabled = false;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "heart");
                }

                else if (currentReaction.Equals("heart"))
                {
                    ZComment.Heart -= 1;
                    HeartBtn.IsEnabled = true;
                    HappyBtn.IsEnabled = true;
                    SadBtn.IsEnabled = true;
                    LikeBtn.IsEnabled = true;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                }
            }

        }

        private void HappyBtn_Click(object sender, RoutedEventArgs e)
        {
            
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Happy += 1;
                HeartBtn.IsEnabled = false;
                SadBtn.IsEnabled = false;
                LikeBtn.IsEnabled = false;
                HappyBtn.IsEnabled = true;
                CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "happy");
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (string.IsNullOrEmpty(currentReaction))
                {
                    ZComment.Happy += 1;
                    HeartBtn.IsEnabled = false;
                    SadBtn.IsEnabled = false;
                    LikeBtn.IsEnabled = false;
                    HappyBtn.IsEnabled = true;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "happy");
                }

                else if (currentReaction.Equals("happy"))
                {
                    ZComment.Happy -= 1;
                    HeartBtn.IsEnabled = true;
                    HappyBtn.IsEnabled = true;
                    SadBtn.IsEnabled = true;
                    LikeBtn.IsEnabled = true;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                }
            }
        }

        private void LikeBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Like += 1;
                HeartBtn.IsEnabled = false;
                SadBtn.IsEnabled = false;
                LikeBtn.IsEnabled = true;
                HappyBtn.IsEnabled = false;
                CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "like");
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (string.IsNullOrEmpty(currentReaction))
                {
                    ZComment.Like += 1;
                    HeartBtn.IsEnabled = false;
                    SadBtn.IsEnabled = false;
                    LikeBtn.IsEnabled = true;
                    HappyBtn.IsEnabled = false;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "like");
                }

                else if (currentReaction.Equals("like"))
                {
                    ZComment.Like-= 1;
                    HeartBtn.IsEnabled = true;
                    HappyBtn.IsEnabled = true;
                    SadBtn.IsEnabled = true;
                    LikeBtn.IsEnabled = true;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                }
            }
        }

        private void SadBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Sad += 1;
                HeartBtn.IsEnabled = false;
                SadBtn.IsEnabled = true;
                LikeBtn.IsEnabled = false;
                HappyBtn.IsEnabled = false;
                CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "sad");
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (string.IsNullOrEmpty(currentReaction))
                {
                    ZComment.Sad += 1;
                    HeartBtn.IsEnabled = false;
                    SadBtn.IsEnabled = true;
                    LikeBtn.IsEnabled = false;
                    HappyBtn.IsEnabled = false;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "sad");
                }

                else if (currentReaction.Equals("sad"))
                {
                    ZComment.Sad-= 1;
                    HeartBtn.IsEnabled = true;
                    HappyBtn.IsEnabled = true;
                    SadBtn.IsEnabled = true;
                    LikeBtn.IsEnabled = true;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                }
            }
        }

        private void InnerReactions_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.InnerReactions.Opacity = 1;
        }

        private void InnerReactions_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            this.InnerReactions.Opacity = 0;
        }
    }

}
