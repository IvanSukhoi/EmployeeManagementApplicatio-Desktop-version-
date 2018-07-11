using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class DepartmentServiceTest
    {
        private IDepartmentRepository _departmentRepository;
        private List<DepartmentModel> _departmentModels;

        [SetUp]
        public void SetUp()
        {
            _departmentRepository = A.Fake<IDepartmentRepository>();
            InitRepository();
        }

        [Test]
        public async Task GetAll_ReturnsEmployees_Correct()
        {
            var expectedValue = await _departmentRepository.GetAllAsync();

            Assert.That(expectedValue.Count, Is.EqualTo(2));
            AssertPropertyValue(expectedValue.First(), _departmentModels.First());
        }

        [Test]
        public async Task GetById_ReturnEmployeeModel_Correct()
        {
            var expectedValue = await _departmentRepository.GetByIdAsync(1);

            AssertPropertyValue(expectedValue, _departmentModels.FirstOrDefault(x => x.Id == 1));
        }

        public void InitRepository()
        {
            _departmentModels = new List<DepartmentModel>
            {
                new DepartmentModel
                {
                    Id = 1,
                    Name = "Department1",
                    QuantityEmployees = 20
                },
                new DepartmentModel
                {
                    Id = 2,
                    Name = "Department2",
                    QuantityEmployees = 21
                }
            };

            A.CallTo(() => _departmentRepository.GetAllAsync()).Returns(_departmentModels);
            A.CallTo(() => _departmentRepository.GetByIdAsync(A<int>.Ignored))
                .ReturnsLazily((int id) => _departmentModels.FirstOrDefault(x => x.Id == id));
        }

        public void AssertPropertyValue(DepartmentModel expectedValue, DepartmentModel departmentModel)
        {
            Assert.That(expectedValue.Id, Is.EqualTo(departmentModel.Id));
            Assert.That(expectedValue.Name, Is.EqualTo(departmentModel.Name));
            Assert.That(expectedValue.QuantityEmployees, Is.EqualTo(departmentModel.QuantityEmployees));
        }
    }
}
