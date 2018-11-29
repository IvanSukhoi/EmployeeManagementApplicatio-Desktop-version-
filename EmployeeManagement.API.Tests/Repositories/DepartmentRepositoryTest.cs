using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
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
        public void GetAllAsync_ReturnsAll_Correct()
        {
            var departmentModels = new List<DepartmentModel>
            {
                new DepartmentModel {Name = "IT", Id = 1, QuantityEmployees = 12},
                new DepartmentModel {Name = "Bokkeeping", Id = 2, QuantityEmployees = 20},
            };

            A.CallTo(() => _webClient.GetAsync<List<DepartmentModel>>(SettingsConfiguration.ApiUrls.Department.GetAll))
                .Returns(departmentModels);

            var departments = _departmentRepository.GetAllAsync().Result;

            Assert.That(2, Is.EqualTo(departments.Count));

            Assert.That("IT", Is.EqualTo(departments.First().Name));
            Assert.That(1, Is.EqualTo(departments.First().Id));
            Assert.That(12, Is.EqualTo(departments.First().QuantityEmployees));
        }

        [Test]
        public void GetAllAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() => _webClient.GetAsync<List<DepartmentModel>>(SettingsConfiguration.ApiUrls.Department.GetAll))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _departmentRepository.GetAllAsync());
        }

        [Test]
        public async Task GetByIdAsync_GetDepartment_Correct()
        {
            var departmentModel = new DepartmentModel
            {
                Name = "IT",
                Id = 1,
                QuantityEmployees = 25
            };
            
            A.CallTo(() => _webClient.GetAsync<DepartmentModel>(SettingsConfiguration.ApiUrls.Department.GetById + "1")).ReturnsLazily(() => departmentModel);

            var department = await _departmentRepository.GetByIdAsync(1);

            Assert.That("IT", Is.EqualTo(department.Name));
            Assert.That(1, Is.EqualTo(department.Id));
            Assert.That(25, Is.EqualTo(department.QuantityEmployees));
        }

        [Test]
        public void GetByIdAsync_InvalidOperatingException_InCorrect()
        {
            A.CallTo(() => _webClient.GetAsync<DepartmentModel>(SettingsConfiguration.ApiUrls.Department.GetById + "-1"))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _departmentRepository.GetByIdAsync(-1));
        }
    }
}
