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

        public static void RemoveTask(long taskId)
        {
            DBAdapter.Connection.Table<TaskModel>().Delete(task => task.TaskId == taskId);
        }
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

        public static ObservableCollection<TaskModel> GetTasksFromId(ObservableCollection<long> taskIds)
        {
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            var query = DBAdapter.Connection.Table<TaskModel>();
            foreach (TaskModel task in query)
            {
                foreach (long Id in taskIds)
                {
                    if (task.TaskId == Id)
                    {
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }
        public static ObservableCollection<TaskModel> GetAssignedTasks(string userName)
        {
            ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
            var query = DBAdapter.Connection.Table<TaskModel>();
            foreach (TaskModel task in query)
            {
               if(task.AssignedByUser.Equals(userName))
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        public static void UpdateStartDate(long taskId,DateTimeOffset sDate)
        {
            var query = DBAdapter.Connection;
            var curTask = query.Table<TaskModel>().Where(task => task.TaskId == taskId).SingleOrDefault();
            curTask.StartDate=sDate;
            query.Update(curTask);
        }
        public static void UpdateEndDate(long taskId, DateTimeOffset eDate)
        {
            var query = DBAdapter.Connection;
            var curTask = query.Table<TaskModel>().Where(task => task.TaskId == taskId).SingleOrDefault();
            curTask.EndDate = eDate;
            query.Update(curTask);
        }
        public static void UpdatePriority(long taskId, int curPriority)
        {
            var query = DBAdapter.Connection;
            var curTask = query.Table<TaskModel>().Where(task => task.TaskId == taskId).SingleOrDefault();
            curTask.Priority = curPriority;
            query.Update(curTask);
        }
        public static void UpdateDescription(long taskId, string description)
        {
            var query = DBAdapter.Connection;
            var curTask = query.Table<TaskModel>().Where(task => task.TaskId == taskId).SingleOrDefault();
            curTask.Description = description;
            query.Update(curTask);
        }
        public static void UpdateAssignedTo(long taskId, string assignedTo)
        {
            var query = DBAdapter.Connection;
            var curTask = query.Table<TaskModel>().Where(task => task.TaskId == taskId).SingleOrDefault();
            curTask.AssignedToUser = assignedTo;
            query.Update(curTask);
        }
    }
}
