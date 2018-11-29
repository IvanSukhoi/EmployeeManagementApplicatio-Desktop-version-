using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class DepartmentRepositoryTest
    {
        private IDepartmentRepository _departmentRepository;

        private IWebClient _webClient;
        private IAuthorizationManager _authorizationManager;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _authorizationManager = A.Fake<IAuthorizationManager>();

            _departmentRepository = new DepartmentRepository(_webClient, _authorizationManager);
        }

        [Test]
        public void GetAllAsync_ReturnsAll_Correct()
        {
            var departmentModels = new List<DepartmentModel>
            {
                new DepartmentModel {Id = 1},
                new DepartmentModel {Id = 2},
            };

            A.CallTo(() => _webClient.GetAsync<List<DepartmentModel>>(SettingsConfiguration.ApiUrls.GetDepartment, true))
                .Returns(departmentModels);

            var departments = _departmentRepository.GetAllAsync().Result;

            Assert.That(2, Is.EqualTo(departments.Count));
            Assert.That(1, Is.EqualTo(departments.First().Id));
        }

        [Test]
        public void GetAllAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() => _webClient.GetAsync<List<DepartmentModel>>(SettingsConfiguration.ApiUrls.GetDepartment, true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _departmentRepository.GetAllAsync());
        }

        [Test]
        public async Task GetByIdAsync_GetDepartment_Correct()
        {
            var departmentModel = new DepartmentModel
            {
                Id = 1,
                QuantityEmployees = 25
            };
            
            A.CallTo(() => _webClient.GetAsync<DepartmentModel>(SettingsConfiguration.ApiUrls.GetDepartment + "1", true)).ReturnsLazily(() => departmentModel);

            var department = await _departmentRepository.GetByIdAsync(1);

            Assert.That(1, Is.EqualTo(department.Id));
            Assert.That(25, Is.EqualTo(department.QuantityEmployees));
        }

        [Test]
        public void GetByIdAsync_InvalidOperatingException_InCorrect()
        {
            A.CallTo(() => _webClient.GetAsync<DepartmentModel>(SettingsConfiguration.ApiUrls.GetDepartment + "-1", true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _departmentRepository.GetByIdAsync(-1));
        }
    }
}
