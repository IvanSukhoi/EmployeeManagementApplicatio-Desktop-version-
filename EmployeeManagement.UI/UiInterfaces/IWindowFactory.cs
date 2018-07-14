using System.Windows;

namespace EmployeeManagement.UI.UiInterfaces
{
    public interface IWindowFactory
    {
        T Create<T>() where T : Window;
        T Get<T>() where T : Window;
        void Close<T>() where T : Window;
    }
}
