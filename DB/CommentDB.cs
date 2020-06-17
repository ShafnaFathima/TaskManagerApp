using System;
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
        public static void AddComment(CommentModel comment)
        {
            DBAdapter.Connection.Insert(comment);
        }

        //private ObservableCollection<CommentModel> _comments = new ObservableCollection<CommentModel>();
        public static ObservableCollection<CommentModel> GetComments(long taskId)
        {
            ObservableCollection<CommentModel> comments
                = new ObservableCollection<CommentModel>();
            var query = DBAdapter.Connection.Table<CommentModel>();
            foreach (CommentModel comment in query)
            {
                if (comment.CommentToTaskId==taskId)
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }
    }
}
