using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApp.Model;

namespace TaskManagerApp.DB
{
    public class TaskDB
    {
        public static void AddTask(TaskModel task)
        {
            DBAdapter.Connection.Insert(task);
        }

        private ObservableCollection<TaskModel> _tasks = new ObservableCollection<TaskModel>();
        public static ObservableCollection<TaskModel> GetTasks(string userName)
        {
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            var query = DBAdapter.Connection.Table<TaskModel>();
            foreach (TaskModel usertask in query)
            {
                if (usertask.AssignedToUser.Equals(userName))
                {
                    tasks.Add(usertask);
                }
            }
            return tasks;
        }
    }
}
