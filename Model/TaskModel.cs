﻿using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class TaskModel
    {
        private readonly long _TaskId = DateTime.Now.Ticks;
        public string TaskName { get; set; }
        public string AssignedToUser { get; set; }
        public string AssignedByUser { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Priority { get; set; }
        public long TaskId
        {
            get
            {
                return _TaskId;
            }
        }
    }   
 

     
}
