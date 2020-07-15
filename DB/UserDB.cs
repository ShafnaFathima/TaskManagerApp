using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using TaskManagerApp.Model;
using Microsoft.SqlServer;
using SQLite.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Specialized;
using Windows.Security.Authentication.Identity.Core;

namespace TaskManagerApp.DB
{

    public class UserDB
    {
        public static void AddUser(string userName,string avatar)
        {
            UserModel user = new UserModel();
            user.Username = userName;
            user.Avatar = avatar;
            DBAdapter.Connection.Insert(user);
        }

        public static UserModel GetUser(string userName)
        {
            var query = DBAdapter.Connection.Table<UserModel>();
            var currentUser = query.Where(user => user.Username.Equals(userName)).SingleOrDefault();
            return (UserModel)currentUser;
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

        public static bool IsFavouriteTask(long taskId, string userName)
        {
            var query = DBAdapter.Connection.Table<FavoriteTask>();
            var favoriteTasks = query.Where(user => user.UserName.Equals(userName))
                                                .Select(user => user.TaskId);

            foreach (long userTaskId in favoriteTasks)
            {
                if (taskId == userTaskId)
                {
                    return true;
                }
            }
            return false;
        }

        public static void AddFavouriteTaskIds(long taskId, string userName)
        {   
            if (!(UserDB.IsFavouriteTask(taskId,userName)))
            {
                FavoriteTask favorite = new FavoriteTask() { TaskId = taskId, UserName = userName, IsFavourite = false };
                DBAdapter.Connection.Insert(favorite);
            }
            else
            {
                var query = DBAdapter.Connection;
                var comment = query.Table<FavoriteTask>().Where(excomment => excomment.TaskId == taskId && excomment.UserName.Equals(userName)).SingleOrDefault();
                comment.IsFavourite = true;
                query.Update(comment);
            }
        }
        public static void RemoveFavouriteTaskIds(long taskId, string userName)
        {
            var query = DBAdapter.Connection;
            var comment = query.Table<FavoriteTask>().Where(excomment => excomment.TaskId == taskId && excomment.UserName.Equals(userName)).SingleOrDefault();
            comment.IsFavourite = false;
            query.Update(comment);
        }

        public static ObservableCollection<long> GetFavTasks(string userName)
        {
            var query = DBAdapter.Connection.Table<FavoriteTask>();
            var FavTaskIds = query.Where(task => task.UserName.Equals(userName) && task.IsFavourite==true)
                                                .Select(task => task.TaskId);
            ObservableCollection<long> FavTasksId = new ObservableCollection<long>(FavTaskIds);
            return FavTasksId;
        }
        public static FavoriteTask GetFavorite(long taskId,string userName)
        {
            FavoriteTask fav = new FavoriteTask();
            var query = DBAdapter.Connection.Table<FavoriteTask>();
            foreach (FavoriteTask task in query)
            {
                
                    if (task.TaskId == taskId && task.UserName.Equals(userName))
                    {
                    fav = task;
                    break;
                    }
                
            }
            return fav;
        }
    }
}
