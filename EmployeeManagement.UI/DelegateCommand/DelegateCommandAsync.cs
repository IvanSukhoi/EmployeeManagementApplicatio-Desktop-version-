using System;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.DelegateCommand
{
    public class DelegateCommandAsync : IDelegateCommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommandAsync(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommandAsync(Func<object, Task> execute)
        {
            _execute = execute;
            _canExecute = AlwaysCanExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool AlwaysCanExecute(object parametr)
        {
            return true;
        }
    }
}
