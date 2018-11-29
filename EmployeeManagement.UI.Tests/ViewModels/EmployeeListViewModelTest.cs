using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    public class EmployeeListViewModelTest
    {
        private IEmployeeService _employeeService;
        private IDepartmentService _departmentService;
        private IMapperWrapper _mapperWrapper;
        private IResourceManagerService _resourceManagerService;
        private EmployeeListViewModel _employeeListViewModel;

        [SetUp]
        public void SetUp()
        {
            _employeeService = A.Fake<IEmployeeService>();
            _departmentService = A.Fake<IDepartmentService>();
            _mapperWrapper = A.Fake<IMapperWrapper>();
            _resourceManagerService = A.Fake<IResourceManagerService>();
            _employeeListViewModel = new EmployeeListViewModel(_employeeService, _mapperWrapper, _departmentService, _resourceManagerService);
            Init();
        }

        [Test]
        public async Task ExecuteCreateEmployee_CreateCurrentEmployeeViewModel()
        {
            _employeeListViewModel.CurrentDepartment = (Departments)2;
            await _employeeListViewModel.ExecuteCreateEmployeeAsync();

            Assert.IsTrue(_employeeListViewModel.CurrentEmployeeViewModel.IsNew);
            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.DepartmentId, Is.EqualTo(2));
            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.DepartmentName, Is.EqualTo("Name1"));
        }

        [Test]
        public async Task UpdateEmpployees_AssignEmployees_Correct()
        {
            await _employeeListViewModel.UpdateEmployees((Departments)2);

            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.DepartmentId, Is.EqualTo(2));
            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.DepartmentName, Is.EqualTo("DepartmentName1"));
        }

        [Test]
        public void UpdateCurrentEmployeeHandler_ChangeDepartment_Correct()
        {
            var employeeViewModel = new EmployeeViewModel
            {
                IsNew = false,
                IsEditedDepartment = true
            };

            _employeeListViewModel.CurrentEmployeeViewModel = employeeViewModel;
            _employeeListViewModel.Employees = new ObservableCollection<EmployeeViewModel> {employeeViewModel, new EmployeeViewModel{Id = 10}};

            _employeeListViewModel.UpdateCurrentEmployeeHandler();

            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.Id, Is.EqualTo(10));
        }

        [Test]
        public void UpdateCurrentEmployeeHandler_RemoveEmployee_Correct()
        {
            var employeeViewModel = new EmployeeViewModel
            {
                IsDeleted = true
            };

            _employeeListViewModel.CurrentEmployeeViewModel = employeeViewModel;
            _employeeListViewModel.Employees = new ObservableCollection<EmployeeViewModel> { employeeViewModel, new EmployeeViewModel { Id = 10 } };

            _employeeListViewModel.UpdateCurrentEmployeeHandler();

            Assert.That(_employeeListViewModel.CurrentEmployeeViewModel.Id, Is.EqualTo(10));
        }

        [Test]
        public void UpdateCurrentEmployeeHandler_AddEmployee_Correct()
        {
            _employeeListViewModel.CurrentEmployeeViewModel = new EmployeeViewModel{Id = 18, IsNew = true};

            _employeeListViewModel.Employees = new ObservableCollection<EmployeeViewModel>();

            _employeeListViewModel.UpdateCurrentEmployeeHandler();

            Assert.That(_employeeListViewModel.Employees.Count, Is.EqualTo(1));
            Assert.IsFalse(_employeeListViewModel.CurrentEmployeeViewModel.IsNew);
        }

        public void Init()
        {
            var employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel
                {
                    DepartmentId = 2,
                    DepartmentName = "DepartmentName1"
                },
                new EmployeeModel
                {
                    DepartmentId = 2,
                    DepartmentName = "DepartmentName2"
                }
            };

            A.CallTo(() => _departmentService.GetByDepartmentIdAsync(A<int>.Ignored)).Returns(new DepartmentModel
            {
                Id = 2,
                Name = "Name1"
            });

            A.CallTo(() => _employeeService.GetByDepartmentIdAsync(A<int>.Ignored))
                .ReturnsLazily((int id) => employeeModels.Where(x => x.DepartmentId == id).ToList());

            A.CallTo(() => _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(A<EmployeeModel>.Ignored))
                .ReturnsLazily(
                    (EmployeeModel employeeModel) => new EmployeeViewModel
                    {
                        DepartmentId = employeeModel.DepartmentId,
                        DepartmentName = employeeModel.DepartmentName
                    });

            A.CallTo(() =>
                    _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(A<List<EmployeeModel>>.Ignored))
                .ReturnsLazily(
                    (List<EmployeeModel> employees) =>
                    {
                        var list = new List<EmployeeViewModel>();
                        employees.ForEach(x => list.Add(_mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(x)));

                        return list;
                    });

            A.CallTo(() => _resourceManagerService.GetString(A<string>.Ignored)).ReturnsLazily((string local) => local);
        }
    }
}
