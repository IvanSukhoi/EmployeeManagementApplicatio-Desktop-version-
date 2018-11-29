using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using Prism.Events;

namespace EmployeeManagement.UI.Events
{
    public class UpdateEmployeeViewModelEvent : PubSubEvent<EmployeeViewModel> { }

    public class SaveEmployeeViewModelEvent : PubSubEvent { }

    public class UpdateMainWindowEvent : PubSubEvent<MainWindow> { }
}
