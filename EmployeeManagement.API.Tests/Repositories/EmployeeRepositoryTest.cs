using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class EmployeeRepositoryTest
    {
        private EmployeeRepository _employeeRepository;
        private IWebClient _webClient;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _employeeRepository = new EmployeeRepository(_webClient);
        }

        [Test]
        public void GetByDepartmentIdAsync_ReturnEmployees_Correct()
        {
            //arrange
            var employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel(){FirstName = "Andrey", Id = 11, DepartmentId = 1, Sex = Sex.Male},
                new EmployeeModel(){FirstName = "Alex", Id = 11, DepartmentId = 1, Sex = Sex.Male}
            };

            A.CallTo(() => _webClient.GetAsync<List<EmployeeModel>>(SettingsConfiguration.ApiUrls.Employee.GetByDepartmentId + "1"))
                .ReturnsLazily(() => employeeModels);

            //act
            var employees = _employeeRepository.GetByDepartmentIdAsync(1).Result;
            
            //assert
            Assert.That(2, Is.EqualTo(employees.Count));

            Assert.That(employees.First().Id, Is.EqualTo(11));
            Assert.That(employees.First().FirstName, Is.EqualTo("Andrey"));
            Assert.That(employees.First().Sex, Is.EqualTo(Sex.Male));
        }

        [Test]
        public void GetByDepartmentIdAsync_InvalidOperationException_InCorrect()
        {
            //arrange
            A.CallTo(() => _webClient.GetAsync<List<EmployeeModel>>(SettingsConfiguration.ApiUrls.Employee.GetByDepartmentId + "-1")).Throws<InvalidOperationException>();

            //act/assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.GetByDepartmentIdAsync(-1));
        }

        [Test]
        public void GetByIdAsync_ReturnEmployeeModel_Correct()
        {
            //arrange
            var employeeModel = new EmployeeModel
            {
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Id = 1,
                DepartmentId = 2,
                DepartmentName = "Sales",
                Sex = Sex.Male,
                Profession = Profession.Manager,
            };

            A.CallTo(() => _webClient.GetAsync<EmployeeModel>(SettingsConfiguration.ApiUrls.Employee.GetbyId + "1")).ReturnsLazily(() => employeeModel);

            //act
            var expectedValue = _employeeRepository.GetByIdAsync(1).Result;

            //assert
            AssertPropertyValue(expectedValue, employeeModel);
        }

        [Test]
        public void GetByIdAsync_InvalidOperatingException_Incorrect()
        {
            //arrange
            A.CallTo(() => _webClient.GetAsync<EmployeeModel>(SettingsConfiguration.ApiUrls.Employee.GetbyId + "-1"))
                .Throws<InvalidOperationException>();

            //act/assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.GetByIdAsync(-1));
        }

        [Test]
        public void CreateAsync_ReturnEmployeeModel_Correct()
        {
            //arrange
            var employeeModel = new EmployeeModel
            {
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Id = 1,
                DepartmentId = 2,
                DepartmentName = "Sales",
                Sex = Sex.Male,
                Profession = Profession.Manager,
            };

            var newEmployeeModel = new EmployeeModel
            {
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                Id = 1,
                DepartmentId = 2,
                DepartmentName = "Sales",
                Sex = Sex.Male,
                Profession = Profession.Manager,
            };

            A.CallTo(() => _webClient.PostAsync<EmployeeModel, EmployeeModel>(SettingsConfiguration.ApiUrls.Employee.Create, employeeModel))
                .ReturnsLazily(() => newEmployeeModel);

            //act
            var expectedValue = _employeeRepository.CreateAsync(employeeModel).Result;

            //assert
            AssertPropertyValue(expectedValue, employeeModel);
        }

        [Test]
        public void CreateAsunc_InvalidOperatingException_InCorrect()
        {
            //arrange
            var employeeModel = new EmployeeModel();

            A.CallTo(() =>
                _webClient.PostAsync<EmployeeModel, EmployeeModel>(SettingsConfiguration.ApiUrls.Employee.Create,
                    employeeModel)).Throws<InvalidOperationException>();

            //act/assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.CreateAsync(employeeModel));
        }

        [Test]
        public void DeleteAsync_RemoveEmployeeModel_Correct()
        {
            //arrange
            A.CallTo(() => _webClient.DeleteAsync(SettingsConfiguration.ApiUrls.Employee.Delete + "1"))
                .ReturnsLazily(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            //act/assert
            Assert.DoesNotThrowAsync(() => _employeeRepository.DeleteAsync(1));
        }

        [Test]
        public void DeleteAsync_InvalidOperationException_InCorrect()
        {
            //arrange
            A.CallTo(() => _webClient.DeleteAsync(SettingsConfiguration.ApiUrls.Employee.Delete + "-1"))
                .Throws<InvalidOperationException>();

            //act/assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.DeleteAsync(-1));
        }

        [Test]
        public void SaveAsync_SaveEmployeeModel_Correct()
        {
            //arrange
            var employeeModel = new EmployeeModel();

            A.CallTo(() => _webClient.PutAsync(SettingsConfiguration.ApiUrls.Employee.Save, employeeModel))
                .ReturnsLazily(() => new HttpResponseMessage {StatusCode = HttpStatusCode.OK});

            //act/assert
            Assert.DoesNotThrowAsync(() => _employeeRepository.SaveAsync(employeeModel));
        }

        [Test]
        public void SaveAsync_InvalidOperatingException_InCorrect()
        {
            //arrange
            var employeeModel = new EmployeeModel();

            A.CallTo(() => _webClient.PutAsync(SettingsConfiguration.ApiUrls.Employee.Save, employeeModel)).Throws<InvalidOperationException>();

            //act/assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeRepository.SaveAsync(employeeModel));
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
