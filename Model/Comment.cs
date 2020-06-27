using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    [Table("Comment")]
    public class Comment
    {   
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        public long CommentId { get; set; }
        public DateTime Date { get; set; }

        /*   public string Content
           {
               get
               {
                   return _content;
               }
               set
               {
                   _content = value;
               }
           }
           public string AuthorName
           {
               get
               {
                   return _authorName;
               }
               set
               {
                   _authorName = value;
               }
           }
           public long CommentToTaskId
           {
               get
               {
                   return _commentToTaskId;
               }
               set
               {
                   _commentToTaskId = value;
               }
           }*/
    }
}
