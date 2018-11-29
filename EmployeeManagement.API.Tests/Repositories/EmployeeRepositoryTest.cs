using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository _employeeRepository;

        private IWebClient _webClient;
        private IAuthorizationManager _authorizationManager;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _authorizationManager = A.Fake<IAuthorizationManager>();

            _employeeRepository = new EmployeeRepository(_webClient, _authorizationManager);
        }

        [Test]
        public async Task GetByDepartmentIdAsync_ReturnEmployees_Correct()
        {
            var employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel(){Id = 1, DepartmentId = 1},
                new EmployeeModel(){Id = 2, DepartmentId = 1}
            };

            A.CallTo(() => _webClient.GetAsync<List<EmployeeModel>>(SettingsConfiguration.ApiUrls.GetEmployeeByDepartmentId + "1", true))
                .ReturnsLazily(() => employeeModels);

            var employees = await _employeeRepository.GetByDepartmentIdAsync(1);
            
            Assert.That(2, Is.EqualTo(employees.Count));

            Assert.IsNotNull(employees.SingleOrDefault(_ => _.Id == 1));
            Assert.IsNotNull(employees.SingleOrDefault(_ => _.Id == 2));
        }

        [Test]
        public void GetByDepartmentIdAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() => _webClient.GetAsync<List<EmployeeModel>>(SettingsConfiguration.ApiUrls.GetEmployeeByDepartmentId + "-1", true)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.GetByDepartmentIdAsync(-1));
        }

        [Test]
        public async Task GetByIdAsync_ReturnEmployeeModel_Correct()
        {
            var employeeModel = new EmployeeModel
            {
                Id = 1,
                DepartmentId = 1,
            };

            A.CallTo(() => _webClient.GetAsync<EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployee + "1", true)).Returns(employeeModel);

            var expectedValue = await _employeeRepository.GetByIdAsync(1);

            Assert.That(expectedValue.DepartmentId, Is.EqualTo(1));
        }

        [Test]
        public void GetByIdAsync_InvalidOperatingException_Incorrect()
        {
            A.CallTo(() => _webClient.GetAsync<EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployee + "-1", true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.GetByIdAsync(-1));
        }

        [Test]
        public async Task CreateAsync_ReturnEmployeeModel_Correct()
        {
            A.CallTo(() => _webClient.PostAsync<EmployeeModel, EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployee, A<EmployeeModel>.Ignored, true))
                .ReturnsLazily(() => new EmployeeModel());

            var expectedValue = await _employeeRepository.CreateAsync(new EmployeeModel());

            Assert.IsNotNull(expectedValue);
        }

        [Test]
        public void CreateAsync_InvalidOperatingException_InCorrect()
        {
            var employeeModel = new EmployeeModel();

            A.CallTo(() =>
                _webClient.PostAsync<EmployeeModel, EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployee,
                    employeeModel, true)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.CreateAsync(employeeModel));
        }

        [Test]
        public void  DeleteAsync_RemoveEmployeeModel_Correct()
        {
            A.CallTo(() => _webClient.DeleteAsync(SettingsConfiguration.ApiUrls.GetEmployee + "1", true)).Returns(new HttpResponseMessage());

            Assert.DoesNotThrowAsync(() => _employeeRepository.DeleteAsync(1));
        }

        [Test]
        public void DeleteAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() => _webClient.DeleteAsync(SettingsConfiguration.ApiUrls.GetEmployee + "-1", true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.DeleteAsync(-1));
        }

        [Test]
        public void SaveAsync_SaveEmployeeModel_Correct()
        {
            var employeeModel = new EmployeeModel();

            A.CallTo(() => _webClient.PutAsync(SettingsConfiguration.ApiUrls.GetEmployee, employeeModel, true))
                .Returns(new HttpResponseMessage());

            Assert.DoesNotThrowAsync(() => _employeeRepository.SaveAsync(employeeModel));
        }

        [Test]
        public void SaveAsync_InvalidOperatingException_InCorrect()
        {
            var employeeModel = new EmployeeModel();

            A.CallTo(() => _webClient.PutAsync(SettingsConfiguration.ApiUrls.GetEmployee, employeeModel, true)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.SaveAsync(employeeModel));
        }
    }
}
