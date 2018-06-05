using System;

namespace EmployeeManagement.UI.DelegateCommand
{
    public class DelegateCommand<T>: IDelegateCommand<T>
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(Action<T> execute)
        {
            _execute = execute;
            _canExecute = this.AlwaysCanExecute;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool AlwaysCanExecute(T param)
        {
            return true;
        }
    }
}
