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

        Reaction myReaction;
        public  void OnLoaded(Comment comment)
        {
            UserModel commentUser = UserDB.GetUser(comment.AuthorName);
            this.AvatarPic.DataContext = commentUser;
            myReaction = CommentDB.GetReactionObject(comment.CommentId, App.CurrentUser);
            this.CurrentReactionBtn.DataContext = myReaction;
        }

        private void RemoveComment_Click(object sender, RoutedEventArgs e)
        {
            RemovePopup.IsOpen = true;   
        }
       private void HeartBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Heart += 1;
                ZComment.Total += 1;
                CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "heart");
                this.myReaction.ReactionType = "heart";
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
               if(currentReaction.Equals("like"))
                {
                    ZComment.Like -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                }
                if (currentReaction.Equals("happy"))
                {
                    ZComment.Happy -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                }
                if (currentReaction.Equals("sad"))
                {
                    ZComment.Sad -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                }
                if (string.IsNullOrEmpty(currentReaction)||!(currentReaction.Equals("heart")))
                {
                    ZComment.Heart+= 1;
                    ZComment.Total += 1;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "heart");
                    this.myReaction.ReactionType = "heart";
                }
            }

        }

        private void HappyBtn_Click(object sender, RoutedEventArgs e)
        {
            
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Happy += 1;
                ZComment.Total += 1;
                CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "happy");
                this.myReaction.ReactionType = "happy";
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (currentReaction.Equals("like"))
                {
                    ZComment.Like -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                }
                if (currentReaction.Equals("heart"))
                {
                    ZComment.Heart -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                }
                if (currentReaction.Equals("sad"))
                {
                    ZComment.Sad -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                }
                if (string.IsNullOrEmpty(currentReaction)||!(currentReaction.Equals("happy")))
                {
                    ZComment.Happy += 1;
                    ZComment.Total += 1;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "happy");
                    this.myReaction.ReactionType = "happy";
                }
            }
        }

        private void LikeBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Like += 1;
                ZComment.Total += 1;
                CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "like");
                this.myReaction.ReactionType = "like";
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (currentReaction.Equals("happy"))
                {
                    ZComment.Happy -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                }
                if (currentReaction.Equals("heart"))
                {
                    ZComment.Heart -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                }
                if (currentReaction.Equals("sad"))
                {
                    ZComment.Sad -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                }
                if (string.IsNullOrEmpty(currentReaction) || !(currentReaction.Equals("like")))
                {
                    ZComment.Like += 1;
                    ZComment.Total += 1;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "like");
                    this.myReaction.ReactionType = "like";
                }
            }
        }

        private void SadBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isReacted = CommentDB.IsReacted(ZComment.CommentId, App.CurrentUser);
            if (isReacted == false)
            {
                ZComment.Sad += 1;
                ZComment.Total += 1;
                CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "sad");
                this.myReaction.ReactionType = "sad";
            }

            else if (isReacted)
            {
                string currentReaction = CommentDB.GetReaction(ZComment.CommentId, App.CurrentUser);
                if (currentReaction.Equals("like"))
                {
                    ZComment.Like -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                }
                if (currentReaction.Equals("heart"))
                {
                    ZComment.Heart -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                }
                if (currentReaction.Equals("happy"))
                {
                    ZComment.Happy -= 1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                }
                if (string.IsNullOrEmpty(currentReaction) || !(currentReaction.Equals("sad")))
                {
                    ZComment.Sad += 1;
                    ZComment.Total += 1;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "sad");
                    this.myReaction.ReactionType = "sad";
                }
            }
        }


        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button).Tag;
            long commentId = (long)tag;
            Comment comment = CommentDB.GetComment(commentId);
          
            foreach (Comment comm in ViewUserTask.comments)
            {
                if (comm.CommentId == commentId)
                {
                    ViewUserTask.comments.Remove(comm);
                    break;
                }
            }
            CommentDB.RemoveComment(commentId);
            RemovePopup.IsOpen = false;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            RemovePopup.IsOpen = false;
        }

        private void CurrentReactionBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ReactionsPopup.IsOpen = true;
        }

        private void CurrentReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            string oldReaction=this.CurrentReactionBtn.Tag.ToString();
            if(!string.IsNullOrEmpty(oldReaction))
            {
                if(oldReaction.Equals("heart"))
                {
                    ZComment.Heart -=1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHeart(ZComment.CommentId, ZComment.Heart);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                    this.myReaction.ReactionType = "";
                }
                if (oldReaction.Equals("happy"))
                {
                    ZComment.Happy -=1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateHappy(ZComment.CommentId, ZComment.Happy);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                    this.myReaction.ReactionType = "";
                }
                if (oldReaction.Equals("sad"))
                {
                    ZComment.Sad -=1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateSad(ZComment.CommentId, ZComment.Sad);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                    this.myReaction.ReactionType = "";
                }
                if (oldReaction.Equals("like"))
                {
                    ZComment.Like -=1;
                    ZComment.Total -= 1;
                    CommentDB.UpdateLike(ZComment.CommentId, ZComment.Like);
                    CommentDB.AddReaction(ZComment.CommentId, App.CurrentUser, "");
                    this.myReaction.ReactionType = "";
                }
            }
        }
    }

}
