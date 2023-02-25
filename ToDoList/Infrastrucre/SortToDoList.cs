using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.ViewModel;

namespace ToDoList.Infrastrucre
{
    internal class SortToDoList
    {
        public static ObservableCollection<ToDoModel> SortToDo(List<ToDoModel> toDoList,SortStatus sortStatus)
        {
            if (sortStatus == SortStatus.All)
                return AllModels(toDoList);
            else if(sortStatus == SortStatus.Active)
                return ActiveModels(toDoList);
            else
                return DoneModels(toDoList);
        }

        private static ObservableCollection<ToDoModel> AllModels(List<ToDoModel> toDoList)
        {
            return new ObservableCollection<ToDoModel>(toDoList);
        }

        private static ObservableCollection<ToDoModel> ActiveModels(List<ToDoModel> toDoList)
        {
            return new ObservableCollection<ToDoModel>(toDoList.Where(item => !item.IsDone).ToList());
        }

        private static ObservableCollection<ToDoModel> DoneModels(List<ToDoModel> toDoList)
        {
            return new ObservableCollection<ToDoModel>(toDoList.Where(item => item.IsDone).ToList());
        }
    }
}
