using System.Windows.Input;

namespace EmployeeManagement.UI.Interfaces
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
