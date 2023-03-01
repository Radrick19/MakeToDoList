using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Data;
using ToDoList.Infrastrucre.Commands.Base;

namespace ToDoList.Model
{
    internal class ToDoModel : INotifyPropertyChanged
    {
        public static event Action<object> DeleteClickEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush IsDoneButtonBackgroundColor
        {
            get { return IsDone ? new SolidColorBrush(Color.FromRgb(52, 168, 83)) : new SolidColorBrush(Color.FromRgb(197, 34, 31)); }
        }

        public long Id { get; set; }

        public string Task { get; set; }

        public bool IsDone { get; set; }

        public ICommand DeleteClick
        {
            get { return new RelayCommand((obj) => DeleteClickEvent(this)); }
        }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ICommand ChangeIsDoneStatus
        {
            get { return new RelayCommand((obj) => {

                if (IsDone)
                    IsDone = false;
                else
                    IsDone = true;
                DbController.ChangeIsDone(this);
                OnPropertyChanged(nameof(IsDoneButtonBackgroundColor));
            });}
        }

        public ToDoModel(string task, long id, bool isDone)
        {
            Task = task;
            IsDone = isDone;
            Id = id;
        }

    }
}
