using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests
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
        public void GetByDepartmentIdAsync_Id_ReturnEmployees()
        {
            //arrange
            List<EmployeeModel> employeeModels = new List<EmployeeModel>
            {
                new EmployeeModel(){FirstName = "Andrey", Id = 11, Sex = Sex.Male},
                new EmployeeModel(){FirstName = "Alex", Id = 11, Sex = Sex.Male}
            };

            A.CallTo(() => _webClient.GetAsync<List<EmployeeModel>>(A<string>.Ignored))
                .ReturnsLazily(() => employeeModels);

            //act
            var employees = _employeeRepository.GetByDepartmentIdAsync(11).Result;
            
            //assert
            Assert.That(2, Is.EqualTo(employees.Count));

            Assert.That(11, Is.EqualTo(employees.First().Id));
            Assert.That("Andrey", Is.EqualTo(employees.First().FirstName));
            Assert.That(Sex.Male, Is.EqualTo(employees.First().Sex));
        }
    }
}
