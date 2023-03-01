using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ToDoList.Data;
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
        public List<ToDoModel> ToDoList
        {
            get
            {
                switch (sortStatus)
                {
                    case SortStatus.All: return DbController.GetAllData();
                    case SortStatus.Active: return DbController.GetActiveData();
                    case SortStatus.Done: return DbController.GetDoneData();
                    default: return null;
                }
            }
            set { toDoList = value; }
        }
        public string TaskText
        {
            get { return taskText; }
            set { taskText = value; }
        }
        public SortStatus SortStatus
        {
            get { return sortStatus; }
            set { Set(ref sortStatus, value, nameof(ToDoList)); }
        }
        private SortStatus sortStatus;
        private string taskText;
        private List<ToDoModel> toDoList;

        public ICommand AddItem
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if (TaskText != null && TaskText != string.Empty)
                    {
                        DbController.AddData(TaskText);
                        Set(ref taskText, string.Empty, nameof(TaskText));
                        OnPropertyChanged(nameof(ToDoList));
                    }
                });
            }
        }

        public ICommand SetAllSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.All;
                });
            }
        }

        public ICommand SetActiveSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.Active;
                });
            }
        }

        public ICommand SetDoneSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.Done;
                });
            }
        }

        private void DeleteItem(object obj)
        {
            ToDoModel item =  obj as ToDoModel;
            DbController.RemoveItem(item.Id);
            OnPropertyChanged(nameof(ToDoList));
        }

        public MainViewModel()
        {
            ToDoModel.DeleteClickEvent += DeleteItem;
            sortStatus = SortStatus.All;
        }
    }
}
