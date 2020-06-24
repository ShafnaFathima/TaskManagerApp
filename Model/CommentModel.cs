using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class CommentModel
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        public long ParentCommentId { get; set; }
        public long CommentId { get; set; }
        
    }
}
