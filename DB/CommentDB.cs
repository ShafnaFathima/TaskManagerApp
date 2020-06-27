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
       public static void AddComment(Comment comment)
        {
            DBAdapter.Connection.Insert(comment);
        }

        public static ObservableCollection<Comment> GetComments(long taskId)
        {
            ObservableCollection<Comment> comments= new ObservableCollection<Comment>();
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
    }
}
