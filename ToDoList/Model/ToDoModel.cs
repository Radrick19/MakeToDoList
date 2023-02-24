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
            set { isDone = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string task;
        private bool isDone;
        private int id;


        public ICommand DeleteClick
        {
            get { return new RelayCommand((obj) => DeleteClickEvent(obj)); }
        }

        public ToDoModel(string task, int id)
        {
            Task = task;
            IsDone = false;
            Id = id;
        }

    }
}
