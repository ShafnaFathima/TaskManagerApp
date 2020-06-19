using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class CommentModel
    {
        private readonly long _CommentId = DateTime.UtcNow.Ticks;
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        public long ParentCommentId { get; set; }
        public long CommentId
        {
            get
            {
                return _CommentId;
            }
        }
    }
}
