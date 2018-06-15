using System.Collections.ObjectModel;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.UI.Extensions;

namespace EmployeeManagement.UI.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<DepartmentModel> Departments { get; set; }

        private readonly DepartmentService _departmentService;
        
        public HomeViewModel(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public void Init()
        {
            Departments = new ObservableCollection<DepartmentModel>();
            Departments.AddRange(_departmentService.GetAllStatistics());
        }
    }
}
