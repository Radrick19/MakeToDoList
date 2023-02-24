using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoList.Infrastrucre.Commands.Base;
using ToDoList.Model;
using ToDoList.ViewModel.Base;

namespace ToDoList.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ToDoModel> ToDoCollection { get; set; }
        public int itemsCount = 0;
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
                    if (TaskText != null && TaskText != string.Empty)
                    {
                        ToDoCollection.Add(new ToDoModel(TaskText, itemsCount));
                        Set(ref taskText, string.Empty, nameof(TaskText));
                        itemsCount++;
                    }
                });
            }
        }

        private void DeleteItemMethod(object obj)
        {
            int findId = (int)obj;
            var deleteItem = SearchCollectionForId(findId);
            ToDoCollection?.Remove(deleteItem);
        }

        private ToDoModel SearchCollectionForId(int id)
        {
            foreach(var item in ToDoCollection)
            {
                if (id == item.Id)
                    return item;
            }
            throw new System.ArgumentException("No argument index");
        }

        public MainViewModel()
        {
            ToDoCollection = new ObservableCollection<ToDoModel>();
            ToDoModel.DeleteClickEvent += DeleteItemMethod;
        }
    }
}
