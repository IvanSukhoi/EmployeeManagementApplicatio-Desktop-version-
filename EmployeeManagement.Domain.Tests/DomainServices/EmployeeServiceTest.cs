using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class EmployeeServiceTest
    {
        private IEmployeeRepository _employeeRepository;
        private List<EmployeeModel> _employeeModels;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            InitRepository();
        }

        [Test]
        public async Task GetByDepartmentIdAsync_RetuntEmployees_Correct()
        {
            var expectedValue = await _employeeRepository.GetByDepartmentIdAsync(1);

            Assert.That(expectedValue.Count, Is.EqualTo(2));
            AssertPropertyValue(expectedValue.First(), _employeeModels.First());
        }

        [Test]
        public async Task GetByIdAsync_ReturnEmployee_Correct()
        {
            var expexpectedValue = await _employeeRepository.GetByIdAsync(1);

            AssertPropertyValue(expexpectedValue, _employeeModels.First());
        }

        [Test]
        public async Task DeleteAsync_RemoveEmployee_Correct()
        {
            _employeeModels.Add(new EmployeeModel
            {
                Id = 3
            });

            await _employeeRepository.DeleteAsync(3);

            Assert.That(_employeeModels.FirstOrDefault(x => x.Id == 3), Is.Null);
        }

        [Test]
        public async Task CreateAsync_CreateEmployeeModel_Correct()
        {
            var employeeModel = new EmployeeModel
            {
                Id = 4,
                FirstName = "Firsaname4",
                MiddleName = "MiddleName4",
                LastName = "LastName4",
                Sex = Sex.Male,
                Profession = Profession.Developer,
                Position = Position.Junior,
                DepartmentId = 4,
                DepartmentName = "Department4"
            };

            var expectedValue = await _employeeRepository.CreateAsync(employeeModel);

            AssertPropertyValue(expectedValue, employeeModel);
        }

        [Test]
        public async Task SaveAsync_SaveEmployee_Correct()
        {
            var employeeModel = new EmployeeModel
            {
                Id = 5,
                FirstName = "Firsaname5",
                MiddleName = "MiddleName5",
                LastName = "LastName5",
                Sex = Sex.Female,
                Profession = Profession.Manager,
                Position = Position.Middle,
                DepartmentId = 5,
                DepartmentName = "Department5"
            };

            await _employeeRepository.SaveAsync(employeeModel);

            AssertPropertyValue(_employeeModels.FirstOrDefault(x => x.Id == employeeModel.Id), employeeModel);
        }

        public void InitRepository()
        {
            _employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel()
                {
                    Id = 1,
                    FirstName = "Firsaname1",
                    MiddleName = "MiddleName1",
                    LastName = "LastName1",
                    Sex = Sex.Male,
                    Profession = Profession.BusinessAnalyst,
                    Position = Position.Senior,
                    DepartmentId = 1,
                    DepartmentName = "Department1"
                },
                new EmployeeModel()
                {
                    Id = 2,
                    FirstName = "Firsaname2",
                    MiddleName = "MiddleName2",
                    LastName = "LastName2",
                    Sex = Sex.Female,
                    Profession = Profession.BusinessAnalyst,
                    Position = Position.Middle,
                    DepartmentId = 1,
                    DepartmentName = "Department2"
                }
            };

            A.CallTo(() =>  _employeeRepository.GetByIdAsync(A<int>.Ignored))
                .ReturnsLazily((int id) => _employeeModels.FirstOrDefault(x => x.Id == id));

            A.CallTo(() => _employeeRepository.GetByDepartmentIdAsync(A<int>.Ignored))
                .ReturnsLazily((int departmentId) => _employeeModels.Where(x => x.DepartmentId ==  departmentId).ToList());

            A.CallTo(() => _employeeRepository.CreateAsync(A<EmployeeModel>.Ignored)).Invokes((EmployeeModel employeeModel) =>
                _employeeModels.Add(employeeModel)).ReturnsLazily(() => _employeeModels.Last());

            A.CallTo(() => _employeeRepository.DeleteAsync(A<int>.Ignored)).Invokes((int id) =>
                _employeeModels.Remove(_employeeModels.First(x => x.Id == id)));

            A.CallTo(() => _employeeRepository.SaveAsync(A<EmployeeModel>.Ignored)).Invokes(
                (EmployeeModel employeeModel) =>
                {
                    _employeeModels.RemoveAll(x => x.Id == employeeModel.Id);
                    _employeeModels.Add(employeeModel);
                });
        }

        public void AssertPropertyValue(EmployeeModel expectedValue, EmployeeModel employeeModel)
        {
            Assert.That(expectedValue.Id, Is.EqualTo(employeeModel.Id));
            Assert.That(expectedValue.FirstName, Is.EqualTo(employeeModel.FirstName));
            Assert.That(expectedValue.MiddleName, Is.EqualTo(employeeModel.MiddleName));
            Assert.That(expectedValue.LastName, Is.EqualTo(employeeModel.LastName));
            Assert.That(expectedValue.DepartmentId, Is.EqualTo(employeeModel.DepartmentId));
            Assert.That(expectedValue.DepartmentName, Is.EqualTo(employeeModel.DepartmentName));
            Assert.That(expectedValue.ManagerId, Is.EqualTo(employeeModel.ManagerId));
            Assert.That(expectedValue.Sex, Is.EqualTo(employeeModel.Sex));
            Assert.That(expectedValue.Profession, Is.EqualTo(employeeModel.Profession));
            Assert.That(expectedValue.Position, Is.EqualTo(employeeModel.Position));
        }
    }
}
