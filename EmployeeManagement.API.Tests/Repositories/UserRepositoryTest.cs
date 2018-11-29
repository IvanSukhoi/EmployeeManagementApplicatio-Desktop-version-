using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        private IWebClient _webClient;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _userRepository = new UserRepository(_webClient);
        }

        [Test]
        public async Task GetUserModelAsync_ReturnUser_Correct()
        {
            var userModel = new UserModel
            {
                Id = 1,
                Login = "Login1",
                Password = "Password1",
                SettingsModel = new SettingsModel()
            };

            A.CallTo(() => _webClient.PostAsync<UserModel, string[]>(SettingsConfiguration.ApiUrls.User.GetUserModel, A<string[]>.Ignored)).Returns(userModel);

            var expectedValue = await _userRepository.GetByLoginAsync("Login1", "Password1");

            AssertPropertyValue(expectedValue, userModel);
        }

        [Test]
        public void GetUserModelAsync_IvalidOperationException_InCorrect()
        {
            A.CallTo(() =>
                _webClient.PostAsync<UserModel, string[]>(SettingsConfiguration.ApiUrls.User.GetUserModel,
                    A<string[]>.Ignored)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(
                () => _userRepository.GetByLoginAsync("Login1", "Password1"));
        }

        public void AssertPropertyValue(UserModel expectedValue, UserModel userModel)
        {
            Assert.That(expectedValue.Id, Is.EqualTo(userModel.Id));
            Assert.That(expectedValue.Login, Is.EqualTo(userModel.Login));
            Assert.That(expectedValue.Password, Is.EqualTo(userModel.Password));
        }
    }
}
