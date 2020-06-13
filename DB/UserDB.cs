using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Windows.System;
using TaskManagerApp.Model;
using Microsoft.SqlServer;
using SQLite.Net;
using System.IO;


namespace TaskManagerApp.DB
{

    public class UserDB
    {
        public static void AddUser(string userName)
        {
            UserModel user = new UserModel();
            user.Username = userName;
            DBAdapter.Connection.Insert(user);
        }

        public static bool CheckValidUser(string userName)
        {
            var users = DBAdapter.Connection.Table<UserModel>();

            if (users.Count() == 0)
            {
                return false;
            }
            else
            {
                var isValid = (from user in users
                               where user.Username.Equals(userName)
                               select user).Any();
                return isValid;
            }
        }

        public static List<UserModel> GetUserList()
        {
            List<UserModel> users = new List<UserModel>();
            var query = DBAdapter.Connection.Table<UserModel>();
            foreach (var user in query)
            {
                users.Add(user);
            }
            return users;
        }
    }
}
