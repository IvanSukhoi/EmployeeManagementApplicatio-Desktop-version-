using System.Windows.Controls;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Pages
{
    public partial class ListEmployeePage : Page
    {
        public ListEmployeePage(DepartmentsViewModel departmentsViewModel)
        {
            InitializeComponent();
            DataContext = departmentsViewModel;
        }
        public void Init(Departments department)
        {
            ((DepartmentsViewModel)DataContext).SetDepartment(department);
        }
    }
}
