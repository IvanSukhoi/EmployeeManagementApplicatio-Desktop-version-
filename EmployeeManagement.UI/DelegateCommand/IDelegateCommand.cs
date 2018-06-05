using System.Windows.Input;

namespace EmployeeManagement.UI.DelegateCommand
{
    public interface IDelegateCommand<T>: ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
