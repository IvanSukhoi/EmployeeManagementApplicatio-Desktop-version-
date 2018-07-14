using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    public class EmployeeDetailsViewModelTest
    {
        private IEmployeeService _employeeService;
        private IDepartmentService _departmentService;

        private IMapperWrapper _mapperWrapper;

        private EmployeeViewModel _viewModel;
        private EmployeeViewModel _currentViewModel;

        private EmployeeDetailsViewModel _employeeDetailsViewModel;

        private bool _eventTrigger;

        [SetUp]
        public void SetUp()
        {
            _employeeService = A.Fake<IEmployeeService>();
            _departmentService = A.Fake<IDepartmentService>();

            _mapperWrapper = A.Fake<IMapperWrapper>();

            _employeeDetailsViewModel = new EmployeeDetailsViewModel(_employeeService, _departmentService, _mapperWrapper);
            Init();
        }

        [Test]
        public void ExecuteEditEmployee_ChangeIsEditing_Employee()
        {
            _employeeDetailsViewModel.ExecuteEditEmployee(new object());
            
            Assert.IsTrue(_employeeDetailsViewModel.IsEditingEmployee);
        }

        [Test]
        public void ExecuteCancel_ChangeIsEditingEmployee()
        {
            _employeeDetailsViewModel.ExecuteCancel(new object());

            Assert.IsFalse(_employeeDetailsViewModel.IsEditingEmployee);
        }

        [Test]
        public async Task ExecuteSaveEmployee_CreateEmployee_Correct()
        {
            await _employeeDetailsViewModel.ExecuteSaveEmployee(new object());

            AssertPropertyValue();
        }

        [Test]
        public async Task ExecuteSaveEmployee_SaveEmployee_ChangeIsEditedDepartment_Correct()
        {
            _viewModel.DepartmentId = 2;

            await TestExecuteSaveEmployee();

            Assert.IsTrue(_viewModel.IsEditedDepartment);
        }

        [Test]
        public async Task ExecuteSaveEmployee_SaveEmployee_NoChangeIsEditedDepartment_Correct()
        {
            await TestExecuteSaveEmployee();

            Assert.IsFalse(_viewModel.IsEditedDepartment);
        }

        [Test]
        public void ExecuteOpenDeletePopup_ChangeIsDeletePopup_Correct()
        {
            _employeeDetailsViewModel.ExecuteOpenDeletePopup(new object());

            Assert.IsTrue(_employeeDetailsViewModel.IsDeletePopupOpen);
        }

        [Test]
        public async Task ExecuteDeleteEmployee_DeleteEmployee_Correct()
        {
            await _employeeDetailsViewModel.ExecuteDeleteEmployee(1);

            A.CallTo(() => _employeeService.DeleteAsync(A<int>.That.Matches(x => x > 0))).MustHaveHappened();
            Assert.IsTrue(_currentViewModel.IsDeleted);
            Assert.IsFalse(_employeeDetailsViewModel.IsDeletePopupOpen);
            Assert.IsTrue(_eventTrigger);
        }

        [Test]
        public void ExecuteCancelToListEmployee_ChangeIsDeletePopupOpen_Correct()
        {
            _employeeDetailsViewModel.ExecuteCancelTolistEmployee(new object());

            Assert.IsFalse(_employeeDetailsViewModel.IsDeletePopupOpen);
        }

        [Test]
        public void SetEmployeeHandler_InitializeCurrentEmployeeViewModel_Correct()
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                Id = 4,
                DepartmentId = 4,
                IsNew = true
            };

            _employeeDetailsViewModel.CurrentEmployeeViewModel = null;

            _employeeDetailsViewModel.SetEmployeeHandler(employeeViewModel);

            Assert.IsTrue(_employeeDetailsViewModel.IsEditingEmployee);
            Assert.That(_employeeDetailsViewModel.CurrentEmployeeViewModel.Id, Is.EqualTo(employeeViewModel.Id));
        }

        [Test]
        public void SetEmployeeHandler_InitializeCurrentEmployeeViewModel_InCsorrect()
        {
            _employeeDetailsViewModel.CurrentEmployeeViewModel = null;

            _employeeDetailsViewModel.SetEmployeeHandler(null);

            Assert.IsNull(_employeeDetailsViewModel.CurrentEmployeeViewModel);
            Assert.IsFalse(_employeeDetailsViewModel.IsEditingEmployee);
        }

        [Test]
        public void AssignEmpoyeeModel_InitializeEmployeeViewModel_Correct()
        {
            _employeeDetailsViewModel.AssignEmployeeModel();

            Assert.That(_employeeDetailsViewModel.EmployeeViewModel.Id,
                Is.EqualTo(_employeeDetailsViewModel.CurrentEmployeeViewModel.Id));
            Assert.That(_employeeDetailsViewModel.EmployeeViewModel.DepartmentId, 
                Is.EqualTo(_employeeDetailsViewModel.CurrentEmployeeViewModel.DepartmentId));
        }

        [Test]
        public void AssignEmployeeModel_InitializeEmployeeViewModel_InCorrect()
        {
            _employeeDetailsViewModel.CurrentEmployeeViewModel = null;
            _employeeDetailsViewModel.EmployeeViewModel = null;

            _employeeDetailsViewModel.AssignEmployeeModel();

            Assert.IsNull(_employeeDetailsViewModel.EmployeeViewModel);
        }

        public async Task TestExecuteSaveEmployee()
        {
            _viewModel.IsNew = false;

            await _employeeDetailsViewModel.ExecuteSaveEmployee(new object());

            A.CallTo(() => _employeeService.SaveAsync(A<EmployeeModel>.Ignored)).MustHaveHappened();
            AssertPropertyValue();
        }

        public void AssertPropertyValue()
        {
            Assert.That(_currentViewModel.Id, Is.EqualTo(_viewModel.Id));
            Assert.That(_currentViewModel.DepartmentId, Is.EqualTo(_viewModel.DepartmentId));
            Assert.IsFalse(_employeeDetailsViewModel.IsEditingEmployee);
            Assert.IsTrue(_eventTrigger);
        }

        public void Init()
        {
            _currentViewModel = new EmployeeViewModel
            {
                Id = 1,
                DepartmentId = 1,
                IsNew = true
            }; 
            
            _viewModel = new EmployeeViewModel
            {
                Id = 1,
                DepartmentId = 1,
                IsNew = true
            };

            _employeeDetailsViewModel.CurrentEmployeeViewModel = _currentViewModel;
            _employeeDetailsViewModel.EmployeeViewModel = _viewModel;

            A.CallTo(() => _mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(A<EmployeeViewModel>.Ignored))
                .ReturnsLazily(
                    (EmployeeViewModel employeeViewModel) => new EmployeeModel
                    {
                        Id = employeeViewModel.Id,
                        DepartmentId = employeeViewModel.DepartmentId,
                    });

            A.CallTo(() => _mapperWrapper.Map(A<EmployeeModel>.Ignored, A<EmployeeViewModel>.Ignored)).Invokes(
                (EmployeeModel original, EmployeeViewModel copy) =>
                {
                    copy.Id = original.Id;
                    copy.DepartmentId = original.DepartmentId;
                });

            A.CallTo(() => _employeeService.CreateAsync(A<EmployeeModel>.Ignored)).ReturnsLazily(
                (EmployeeModel employeeModel) => employeeModel);

            A.CallTo(() => _mapperWrapper.Map(A<EmployeeViewModel>.Ignored, A<EmployeeViewModel>.Ignored))
                .Invokes(
                    (EmployeeViewModel original, EmployeeViewModel copy) =>
                    {
                        copy.Id = original.Id;
                        copy.DepartmentId = original.DepartmentId;
                    });

            A.CallTo(() => _mapperWrapper.Map<EmployeeViewModel, EmployeeViewModel>(A<EmployeeViewModel>.Ignored))
                .ReturnsLazily(
                    (EmployeeViewModel employeeViewModel) => new EmployeeViewModel
                    {
                        Id = employeeViewModel.Id,
                        DepartmentId = employeeViewModel.DepartmentId
                    });

            _employeeDetailsViewModel.UpdateEmployeeHandler += () => _eventTrigger = true;
        }
    }
}

