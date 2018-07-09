using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests
{
    public class DepartmentRepositoryTest
    {
        private DepartmentRepository _departmentRepository;
        private IWebClient _webClient;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _departmentRepository = new DepartmentRepository(_webClient);
        }

        [Test]
        public void GetAllAsync_ReturnsAll()
        {
            //arrange
            List<DepartmentModel> departmentModels = new List<DepartmentModel>
            {
                new DepartmentModel {Name = "IT", Id = 1, QuantityEmployees = 12},
                new DepartmentModel {Name = "Bokkeeping", Id = 2, QuantityEmployees = 20},
            };

            A.CallTo(() => _webClient.GetAsync<List<DepartmentModel>>(A<string>.Ignored))
                .ReturnsLazily(() => departmentModels);

            //act
            var departments = _departmentRepository.GetAllAsync().Result;

            //assert
            Assert.That(2, Is.EqualTo(departments.Count));

            Assert.That("IT", Is.EqualTo(departments.First().Name));
            Assert.That(1, Is.EqualTo(departments.First().Id));
            Assert.That(12, Is.EqualTo(departments.First().QuantityEmployees));
        }

        [Test]
        public void GetByIdAsync_Id_GetDepartment()
        {
            //arrange
            var department = new DepartmentModel
            {
                Name = "IT",
                Id = 1,
                QuantityEmployees = 25
            };
            
            //act
            A.CallTo(() => _webClient.GetAsync<DepartmentModel>(A<string>.Ignored)).ReturnsLazily(() => department);

            //assert
            Assert.That("IT", Is.EqualTo(department.Name));
            Assert.That(1, Is.EqualTo(department.Id));
            Assert.That(25, Is.EqualTo(department.QuantityEmployees));
        }
    }
}
