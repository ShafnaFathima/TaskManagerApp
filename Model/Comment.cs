using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TaskManagerApp.Model
{
    [Table("Comment")]
    public class Comment:INotifyPropertyChanged
    {   
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommentToTaskId { get; set; }
        [PrimaryKey]
        public long CommentId { get; set; }
        public DateTime Date { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private int _heart;
        public int Heart
        {
            get
            {
                return _heart;
            }
            set
            {
                _heart = value;
                OnPropertyChanged("Heart");
            }
        }
        private int _like;
        public int Like
        {
            get
            {
                return _like;
            }
            set
            {
                _like = value;
                OnPropertyChanged("Like");
            }
        }
        private int _sad;
        public int Sad
        {
            get
            {
                return _sad;
            }
            set
            {
                _sad = value;
                OnPropertyChanged("Sad");
            }
        }
        private int _happy;
        public int Happy
        {
            get
            {
                return _happy;
            }
            set
            {
                _happy = value;
                OnPropertyChanged("Happy");
            }
        }
        private int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = _happy + _heart + _sad + _like;
                OnPropertyChanged("Total");
                OnPropertyChanged("Happy");
                OnPropertyChanged("Sad");
                OnPropertyChanged("Like");
                OnPropertyChanged("Heart");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
