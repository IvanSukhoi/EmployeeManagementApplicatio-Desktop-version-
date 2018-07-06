using System.Threading.Tasks;
using EmployeeManagement.Contacts.Enums;
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

        public async Task InitAsync(Departments department)
        {
            await ((DepartmentsViewModel)DataContext).InitAsync(department);
        }
    }
}
