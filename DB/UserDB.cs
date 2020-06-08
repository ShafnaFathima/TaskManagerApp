using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Windows.System;
using TaskManagerApp.Model;

namespace TaskManagerApp.DB
{
    class UserDB
    {
        private static List<UserModel> _userList = new List<UserModel>();

        public static void AddUser(string userName)
        {
            UserModel user = new UserModel();
            user.Username = userName;
            _userList.Add(user);
        }

        public static bool CheckValidUser(string userName)
        {
            bool isValid = (from user in _userList
                            where user.Username.Equals(userName)
                            select user).Any();
            return isValid;
        }    
    }
}
