using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Infrastrucre.Commands.Base;
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
        public ICommand AddItem
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(TaskText != null && TaskText != string.Empty)
                    {
                        ToDoCollection.Add(new ToDoModel(TaskText));
                        Set(ref taskText, string.Empty, nameof(TaskText));
                    }
                });
            }
        }
        public ICommand DeleteItem
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    ToDoModel model = obj as ToDoModel;
                    ToDoCollection.Remove(model);
                });
            }
        }
        public MainViewModel()
        {
            ToDoCollection = new ObservableCollection<ToDoModel>();
        }
    }
}
