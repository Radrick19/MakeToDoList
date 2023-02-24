using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.ViewModel.Base;

namespace ToDoList.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoModel> ToDoCollection { get; set; }
        private string taskText;
        public string TaskText
        {
            get { return taskText; }
            set { taskText = value; }
        }
        public MainViewModel()
        {
            ToDoCollection = new ObservableCollection<ToDoModel>();
            ToDoCollection.Add(new ToDoModel("Loxasdaskd kasdk;aslkdl; askldk ;askdl ksl;dklask ;kasl; dkasl;k"));
            ToDoCollection.Add(new ToDoModel("Grox"));
            ToDoCollection.Add(new ToDoModel("Work"));
        }
    }
}
