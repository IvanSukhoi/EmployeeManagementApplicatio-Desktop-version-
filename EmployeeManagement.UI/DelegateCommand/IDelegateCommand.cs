using System.Windows.Input;

namespace EmployeeManagement.UI.DelegateCommand
{
    public interface IDelegateCommand: ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
