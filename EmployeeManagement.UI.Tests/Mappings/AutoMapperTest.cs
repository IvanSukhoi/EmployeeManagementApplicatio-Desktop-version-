using AutoMapper;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Mappings;
using EmployeeManagement.UI.ViewModels;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.Mappings
{
    [TestFixture]
    public class AutoMapperTest
    {
        private IMapperWrapper _mapperWrapper;
        private EmployeeModel _employeeModel;
        private EmployeeViewModel _employeeViewModel;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mapperWrapper = new MapperWrapper();
            Init();
        }

        [Test]
        public void Automapper_MappAllProperties_Correct()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapper_ConvertFromEmployeeModel_EmployeeViewModel_Correct()
        {
            var employeeViewModel = _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeModel);

            AssertPropertyValue(_employeeModel, employeeViewModel);   
        }

        [Test]
        public void AutoMapper_ConvertFromEmployeeViewModel_EmployeeModel_Correct()
        {
            var employeeModel = _mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(_employeeViewModel);

            AssertPropertyValue(employeeModel, _employeeViewModel);
        }

        [Test]
        public void AutoMapper_CopyEmployeeViewModel_Correct()
        {
            var employeeViewModel = new EmployeeViewModel();

            _mapperWrapper.Map(_employeeViewModel, employeeViewModel);

            Assert.That(employeeViewModel.Id, Is.EqualTo(_employeeViewModel.Id));
            Assert.That(employeeViewModel.FirstName, Is.EqualTo(_employeeViewModel.FirstName));
            Assert.That(employeeViewModel.MiddleName, Is.EqualTo(_employeeViewModel.MiddleName));
            Assert.That(employeeViewModel.LastName, Is.EqualTo(_employeeViewModel.LastName));
            Assert.That(employeeViewModel.DepartmentId, Is.EqualTo(_employeeViewModel.DepartmentId));
            Assert.That(employeeViewModel.DepartmentName, Is.EqualTo(_employeeViewModel.DepartmentName));
            Assert.That(employeeViewModel.ManagerId, Is.EqualTo(_employeeViewModel.ManagerId));
            Assert.That(employeeViewModel.Position, Is.EqualTo(_employeeViewModel.Position));
            Assert.That(employeeViewModel.Profession, Is.EqualTo(_employeeViewModel.Profession));
            Assert.That(employeeViewModel.Sex, Is.EqualTo(_employeeViewModel.Sex));
            Assert.IsTrue(employeeViewModel.IsDeleted);
            Assert.IsTrue(employeeViewModel.IsNew);
            Assert.IsTrue(employeeViewModel.IsEditedDepartment);
        }

        [Test]
        public void AutoMapper_CopyEmployeeModel_Correct()
        {
            var employeeModel = new EmployeeModel();

            _mapperWrapper.Map(_employeeModel, employeeModel);

            Assert.That(employeeModel.Id, Is.EqualTo(_employeeModel.Id));
            Assert.That(employeeModel.FirstName, Is.EqualTo(_employeeModel.FirstName));
            Assert.That(employeeModel.MiddleName, Is.EqualTo(_employeeModel.MiddleName));
            Assert.That(employeeModel.LastName, Is.EqualTo(_employeeModel.LastName));
            Assert.That(employeeModel.DepartmentId, Is.EqualTo(_employeeModel.DepartmentId));
            Assert.That(employeeModel.DepartmentName, Is.EqualTo(_employeeModel.DepartmentName));
            Assert.That(employeeModel.ManagerId, Is.EqualTo(_employeeModel.ManagerId));
            Assert.That(employeeModel.Position, Is.EqualTo(_employeeModel.Position));
            Assert.That(employeeModel.Profession, Is.EqualTo(_employeeModel.Profession));
            Assert.That(employeeModel.Sex, Is.EqualTo(_employeeModel.Sex));
        }

        [Test]
        public void AutoMapper_CopyFromEmployeeModelToEmployeeViewModel()
        {
            var employeeViewModel = new EmployeeViewModel();

            _mapperWrapper.Map(_employeeModel, employeeViewModel);

            AssertPropertyValue(_employeeModel, employeeViewModel);
        }

        [Test]
        public void AutoMapper_CopyFromEmployeeViewModelToEmployeeModel()
        {
            var employeeModel = new EmployeeModel();

            _mapperWrapper.Map(employeeModel, _employeeViewModel);

            AssertPropertyValue(employeeModel, _employeeViewModel);
        }

        public void AssertPropertyValue(EmployeeModel employeeModel, EmployeeViewModel employeeViewModel) 
        {
            Assert.That(employeeModel.Id, Is.EqualTo(employeeViewModel.Id));
            Assert.That(employeeModel.FirstName, Is.EqualTo(employeeViewModel.FirstName));
            Assert.That(employeeModel.MiddleName, Is.EqualTo(employeeViewModel.MiddleName));
            Assert.That(employeeModel.LastName, Is.EqualTo(employeeViewModel.LastName));
            Assert.That(employeeModel.DepartmentId, Is.EqualTo(employeeViewModel.DepartmentId));
            Assert.That(employeeModel.DepartmentName, Is.EqualTo(employeeViewModel.DepartmentName));
            Assert.That(employeeModel.ManagerId, Is.EqualTo(employeeViewModel.ManagerId));
            Assert.That(employeeModel.Position, Is.EqualTo(employeeViewModel.Position));
            Assert.That(employeeModel.Profession, Is.EqualTo(employeeViewModel.Profession));
            Assert.That(employeeModel.Sex, Is.EqualTo(employeeViewModel.Sex));
        }

        public void Init()
        {
            _employeeViewModel = new EmployeeViewModel
            {
                Id = 3,
                FirstName = "FirstName3",
                MiddleName = "MiddleName3",
                LastName = "LastName3",
                DepartmentId = 3,
                DepartmentName = "DepartmentName3",
                ManagerId = 3,
                Position = Position.Intern,
                Profession = Profession.Developer,
                Sex = Sex.Female,
                IsNew = true,
                IsDeleted = true,
                IsEditedDepartment = true
            };

            _employeeModel = new EmployeeModel
            {
                Id = 4,
                FirstName = "FirstName4",
                MiddleName = "MiddleName4",
                LastName = "LastName4",
                DepartmentId = 4,
                DepartmentName = "DepartmentName4",
                ManagerId = 4,
                Position = Position.Intern,
                Profession = Profession.Developer,
                Sex = Sex.Female,
            };
        }
    }
}
