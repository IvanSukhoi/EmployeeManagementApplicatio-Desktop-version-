using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Pages
{
    public partial class DepartmentsPage
    {
        public DepartmentsPage(DepartmentsViewModel departmentsViewModel)
        {
            InitializeComponent();
            DataContext = departmentsViewModel;
        }

        public void Init(Departments department)
        {
            ((DepartmentsViewModel)DataContext).Init(department);
        }
    }
}
