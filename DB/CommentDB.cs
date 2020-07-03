﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApp.Model;
using System.Collections.ObjectModel;

namespace TaskManagerApp.DB
{
    public class CommentDB
    {
        public static void AddComment(Comment comment)
        {
            DBAdapter.Connection.Insert(comment);
        }

        public static ObservableCollection<Comment> GetComments(long taskId)
        {
            ObservableCollection<Comment> comments = new ObservableCollection<Comment>();
            var query = DBAdapter.Connection.Table<Comment>();
            foreach (Comment comment in query)
            {
                if (comment.CommentToTaskId == taskId)
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }

        public static void RemoveComment(long commentId)
        {
            DBAdapter.Connection.Table<Comment>().Delete(comment => comment.CommentId == commentId);
        }
        public static Comment GetComment(long commentId)
        {
            var query = DBAdapter.Connection.Table<Comment>();
            Comment reqComment = new Comment();
            foreach (Comment comment in query)
            {
                if (comment.CommentId == commentId)
                {
                    reqComment = comment;
                }
            }
            return reqComment;
        }
        public static bool IsMyComment(long commentId, string userName)
        {
            var query = DBAdapter.Connection.Table<Comment>();
            var userComments = query.Where(user => user.AuthorName.Equals(userName))
                                                .Select(user => user.CommentId);
            foreach (long userCommentId in userComments)
            {
                if (userCommentId == commentId)
                {
                    return true;
                }
            }
            return false;
        }
        public static void AddHeart(long commentId, int heartCount)
        {
            var query = DBAdapter.Connection;
            var comment = query.Table<Comment>().Where(excomment => excomment.CommentId == commentId).SingleOrDefault();
            comment.Heart = heartCount;
            query.Update(comment);
        }
        public static void AddHappy(long commentId, int happyCount)
        {
            var query = DBAdapter.Connection;
            var comment = query.Table<Comment>().Where(excomment => excomment.CommentId == commentId).SingleOrDefault();
            comment.Happy = happyCount;
            query.Update(comment);
        }
        public static void AddSad(long commentId, int sadCount)
        {
            var query = DBAdapter.Connection;
            var comment = query.Table<Comment>().Where(excomment => excomment.CommentId == commentId).SingleOrDefault();
            comment.Sad = sadCount;
            query.Update(comment);
        }
        public static void AddLike(long commentId, int likeCount)
        {
            var query = DBAdapter.Connection;
            var comment = query.Table<Comment>().Where(excomment => excomment.CommentId == commentId).SingleOrDefault();
            comment.Like = likeCount;
            query.Update(comment);
        }
        public static void AddReaction(long commentId,string userName,string reaction)
        {
            Reaction newReaction = new Reaction() { CommentId = commentId, UserName = userName,ReactionType=reaction };
            DBAdapter.Connection.Insert(newReaction);
        }
        public static bool IsReacted(long commentId, string userName)
        {
            var query = DBAdapter.Connection.Table<Reaction>();
            var userComments = query.Where(user => user.UserName.Equals(userName))
                                                .Select(user => user.CommentId);
            foreach (long userCommentId in userComments)
            {
                if (userCommentId == commentId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
