using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Infrastrucre;
using ToDoList.Infrastrucre.Commands.Base;
using ToDoList.Model;
using ToDoList.ViewModel.Base;

namespace ToDoList.ViewModel
{
    enum SortStatus
    {
        All,
        Active,
        Done
    }
    internal class MainViewModel : BaseViewModel
    {
        private List<ToDoModel> ToDoList { get; set; }
        //хочется чтобы ObservableCollecion сама обновлялась через Set, но при этом в Get выдавала статический метод инфраструктуры
        public ObservableCollection<ToDoModel> ToDoCollection
        {
            get
            {
                ObservableCollection<ToDoModel> collection = SortToDoList.SortToDo(ToDoList, sortStatus);
                return collection;
            }
        }
        private string taskText;
        private SortStatus sortStatus { get; set; }
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
                    if (TaskText != null && TaskText != string.Empty)
                    {
                        ToDoList.Add(new ToDoModel(TaskText));
                        Set(ref taskText, string.Empty, nameof(TaskText));
                        OnPropertyChanged(nameof(ToDoCollection));
                    }
                });
            }
        }

        public ICommand ChangeSortStatus
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    string buttonName = obj as string;
                    if (buttonName == "All")
                        sortStatus = SortStatus.All;
                    else if (buttonName == "Active")
                        sortStatus = SortStatus.Active;
                    else
                        sortStatus = SortStatus.Done;
                    OnPropertyChanged(nameof(ToDoCollection));
                });
            }
        }

        private void DeleteItem(object obj)
        {
            ToDoModel item =  obj as ToDoModel;
            ToDoCollection?.Remove(item);
            //OnPropertyChanged(nameof(ToDoCollection));
        }


        public MainViewModel()
        {
            ToDoList = new List<ToDoModel>();
            ToDoModel.DeleteClickEvent += DeleteItem;
            sortStatus = SortStatus.All;
        }
    }
}
