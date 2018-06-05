using System.Windows.Controls;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Pages
{
    public partial class ListEmployeePage : Page
    {
        public ListEmployeePage(EmployeeListViewModel employeeListViewModel)
        {
            InitializeComponent();
            DataContext = employeeListViewModel;
        }
        public void Init(Departments department)
        {
            ((EmployeeListViewModel)DataContext).SetEmployees(department);
        }
    }
}
