using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Microsoft.SqlServer;
using System.IO;
using TaskManagerApp.Model;

namespace TaskManagerApp.DB
{
    public class DBAdapter
    {
        protected static string DBpath;
        public static SQLite.Net.SQLiteConnection Connection;
        public static void InitializeConnection()
        {
            DBpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "newdb2.sqlite");
            Connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBpath);
            Connection.CreateTable<UserModel>();
            Connection.CreateTable<TaskModel>();
            Connection.CreateTable<CommentModel>();
            Connection.CreateTable<FavoriteTask>();
        }
    }
}

