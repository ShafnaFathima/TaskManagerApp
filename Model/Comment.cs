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
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        [PrimaryKey]
        public long CommentId { get; set; }
        public DateTime Date { get; set; }
        
    }
}
