using System;
using System.Windows.Input;
using ToDoList.Infrastrucre.Commands.Base;

namespace ToDoList.Model
{
    internal class ToDoModel
    {
        public static event Action<object> DeleteClickEvent;
        public string Task
        {
            get { return task; }
            set { task = value; }
        }

        public bool IsDone
        {
            get { return isDone; }
            set { isDone = value;}
        }

        private string task;
        private bool isDone;


        public ICommand DeleteClick
        {
            get { return new RelayCommand((obj) => DeleteClickEvent(this)); }
        }

        public ICommand ChangeIsDoneStatus
        {
            get
            {
                return new RelayCommand((obj) =>
            {
                if (IsDone)
                    IsDone = false;
                else
                    IsDone = true;
            });
            }
        }

        public ToDoModel(string task)
        {
            Task = task;
            IsDone = false;
        }

    }
}
