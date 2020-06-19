using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class FavoriteTask
    {   [PrimaryKey,AutoIncrement]    
        public int Id { get; set; }
        public string UserName { get; set; }
        public long TaskId { get; set; }
    }
}
