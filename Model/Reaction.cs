using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TaskManagerApp.Model
{
    public class Reaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long CommentId { get; set; }
        public string UserName{ get; set; }
        public string ReactionType { get; set; }
    }
}
