using System.Windows;

namespace EmployeeManagement.UI.UiInterfaces.Services
{
    public interface IDialogService
    {
        void ShowMessageBox(string title, string text);
        void ShowMessageBox(string title, string text, MessageBoxButton button);
    }
}
