using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{   
    [Table("FavoriteTasks")]
    public class FavoriteTask:INotifyPropertyChanged
    {   [PrimaryKey,AutoIncrement]    
        public int Id { get; set; }
        public string UserName { get; set; }
        public long TaskId { get; set; }
        private bool _isFavourite;
        public bool IsFavourite
        {
            get
            {
                return _isFavourite;
            }
            set
            {
                _isFavourite = value;
                OnPropertyChanged("IsFavourite");
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
