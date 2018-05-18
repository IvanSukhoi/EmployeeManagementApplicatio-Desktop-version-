using System;
using EmployeeManagement.UI.Interfaces;

namespace EmployeeManagement.UI.DelegateCommand
{
    public class DelegateCommand : IDelegateCommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = this.AlwaysCanExecute;
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
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private bool AlwaysCanExecute(object param)
        {
            return true;
        }
    }
}
