using System;
using System.Windows.Input;

namespace ToDoList.Infrastrucre.Commands.Base
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
    }
}
