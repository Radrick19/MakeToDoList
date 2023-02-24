namespace ToDoList.Model
{
    internal class ToDoModel
    {
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
        private string task;
        private bool isDone;

        public ToDoModel(string task)
        {
            Task = task;
            IsDone = false;
        }
    }
}
