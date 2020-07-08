using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TaskManagerApp.Model
{
    public class Reaction:INotifyPropertyChanged
    {
        private string _reactionType;
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long CommentId { get; set; }
        public string UserName{ get; set; }
        public string ReactionType
        {
            get
            {
                return _reactionType;
            }
            set
            {
                _reactionType = value;
                OnPropertyChanged("ReactionType");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
