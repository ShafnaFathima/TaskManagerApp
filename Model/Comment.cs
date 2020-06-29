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
       
        //public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        [PrimaryKey]
        public long CommentId { get; set; }
        public DateTime Date { get; set; }
        //public Action<Comment> OnRemoveCallback { get; set; }

        //public void OnRemove()
        //{
        //    OnRemoveCallback(this);
        //}


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
