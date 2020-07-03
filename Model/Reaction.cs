using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class Reaction
    {
        public long CommentId { get; set; }
        public string UserName{ get; set; }
        public string ReactionType { get; set; }
    }
}
