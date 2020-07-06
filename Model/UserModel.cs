using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace TaskManagerApp.Model
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }

    }
}
