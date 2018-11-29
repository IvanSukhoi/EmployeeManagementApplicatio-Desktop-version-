using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;

        private IWebClient _webClient;
        private IAuthorizationManager _authorizationManager;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _authorizationManager = A.Fake<IAuthorizationManager>();

            _userRepository = new UserRepository(_webClient, _authorizationManager);
        }

        [Test]
        public async Task GetByLoginAsync_ReturnUserModel_Correct()
        {
            var userModel = new UserModel
            {
                Id = 1,
                Login = "Login1",
            };

            A.CallTo(() => _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUser, "Login1", true)).Returns(userModel);

            var expectedValue = await _userRepository.GetByLoginAsync("Login1");

            Assert.IsNotNull(expectedValue);
        }

        [Test]
        public void GetByLoginAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() =>
                _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUser,
                    "Login1", true)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(
                () => _userRepository.GetByLoginAsync("Login1"));
        }

        [Test]
        public async Task GetByRefreshTokenAsync_ReturnUserModel_Correct()
        {
            var userModel = new UserModel
            {
                Id = 1
            };

            A.CallTo(() =>
                _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUserByRefreshToken,
                    "refreshToken", true)).Returns(userModel);

            var expectedValue = await _userRepository.GetByRefreshTokenAsync("refreshToken");

            Assert.IsNotNull(expectedValue);
            Assert.That(expectedValue.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetByRefreshTokenAsync_InvalidOperationException_Correct()
        {
            A.CallTo(() =>
                _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUserByRefreshToken,
                    "refreshToken", true)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _userRepository.GetByRefreshTokenAsync("refreshToken"));
        }
    }
}
