using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class UserServiceTest
    {
        private IUserRepository _userRepository;
        private List<UserModel> _userModels;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            InitRepository();
        }

        [Test]
        public async Task GetUserModelAsync_ReturnUser_Correct()
        {
            var expectedValue = await _userRepository.GetByLoginAsync("Login1", "Password1");

            AssertPropertyValue(expectedValue, _userModels.FirstOrDefault(x => x.Login == "Login1" && x.Password == "Password1"));
        }

        public void AssertPropertyValue(UserModel expectedValue, UserModel userModel)
        {
            Assert.That(expectedValue.Id, Is.EqualTo(userModel.Id));
            Assert.That(expectedValue.Login, Is.EqualTo(userModel.Login));
            Assert.That(expectedValue.Password, Is.EqualTo(userModel.Password));
        }

        public void InitRepository()
        {
            _userModels = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Login = "Login1",
                    Password = "Password1",
                    SettingsModel = new SettingsModel()
                },
                new UserModel
                {
                    Id = 2,
                    Login = "Login2",
                    Password = "Password2",
                    SettingsModel = new SettingsModel()
                }
            };

            A.CallTo(() => _userRepository.GetByLoginAsync(A<string>.Ignored, A<string>.Ignored)).ReturnsLazily(
                (string login, string password) =>
                    _userModels.FirstOrDefault(x => x.Login == login && x.Password == password));
        }
    }
}
