using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Managers;
using EmployeeManagement.API.Token;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Managers
{
    public class AuthorizationManagerTest
    {
        private IAuthorizationManager _authorizationManager;

        private IWebClient _webClient;
        private IRegistryManager _registryManager;
        private JsonWebToken _jsonWebToken;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _registryManager = A.Fake<IRegistryManager>();

            _authorizationManager = new AuthorizationManager(_webClient, _registryManager);

            Init();
        }

        [Test]
        public async Task SetAuthorizationAsync_InitializeJsonWebTokenViaUserModel_Correct()
        {
            var userModel = new UserModel();

            await _authorizationManager.SetAuthorizationAsync(userModel);

            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, UserModel>(SettingsConfiguration.ApiUrls.GetToken, userModel,
                    false)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task SetAuthorizationAsync_InitializeJsonWebTokenViaRefreshToken_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync(_jsonWebToken.RefreshToken);

            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, string>(SettingsConfiguration.ApiUrls.GetRefreshToken, _jsonWebToken.RefreshToken,
                    false)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void SetAuthorizationAsync_UserModel_InvalidOperationException_InCorrect()
        {
            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, UserModel>(SettingsConfiguration.ApiUrls.GetToken,
                    A<UserModel>.Ignored, false)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(
                () => _authorizationManager.SetAuthorizationAsync(new UserModel()));
        }

        [Test]
        public void SetAuthorizationAsync_RefreshToken_InvalidOperationException_InCorrect()
        {
            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, string>(SettingsConfiguration.ApiUrls.GetRefreshToken,
                    A<string>.Ignored, false)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(
                () => _authorizationManager.SetAuthorizationAsync("1234"));
        }

        [Test]
        public async Task UpdateAuthorizationAsync_UpdateJsonWebToken_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync(_jsonWebToken.RefreshToken);

            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, string>(
                    SettingsConfiguration.ApiUrls.GetRefreshToken, _jsonWebToken.RefreshToken,
                        false)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void UpdateAuthorizationAsync_InvalidOperationException_InCorrect()
        {
            A.CallTo(() =>
                _webClient.PostAsync<JsonWebToken, string>(
                    SettingsConfiguration.ApiUrls.GetRefreshToken, A<string>.Ignored, false)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(
                () => _authorizationManager.SetAuthorizationAsync("1234"));
        }

        [Test]
        public async Task IsAuthorized_True_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync("1234");

            var isAuthorized = _authorizationManager.IsAuthorized();

            Assert.IsTrue(isAuthorized);
        }

        [Test]
        public void IsAuthorized_False_InCorrect()
        {
            var isAuthorized = _authorizationManager.IsAuthorized();

            Assert.IsFalse(isAuthorized);
        }

        [Test]
        public async Task IsValidAccessToken_True_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync("1234");

            var isValidAccessToken = _authorizationManager.IsValidAccessToken();

            Assert.IsTrue(isValidAccessToken);
        }

        [Test]
        public async Task IsValidAccessToken_False_Correct()
        {
            _jsonWebToken.Expires = DateTime.UtcNow.AddDays(-1);

            await _authorizationManager.SetAuthorizationAsync("1234");

            var isValidAccessToken = _authorizationManager.IsValidAccessToken();

            Assert.IsFalse(isValidAccessToken);
        }

        [Test]
        public void IsValidAccessToken_NullReferenceException_InCorrect()
        {
            Assert.Throws<NullReferenceException>(() => _authorizationManager.IsValidAccessToken());
        }

        [Test]
        public async Task GetRefreshToken_ReturnRefreshToken_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync("1234");

            var expectedValue = _authorizationManager.GetRefreshToken();

            Assert.That(expectedValue, Is.EqualTo("5678"));
        }

        [Test]
        public void GetRefreshToken_NullReferenceException_InCorrect()
        {
            Assert.Throws<NullReferenceException>(() => _authorizationManager.GetRefreshToken());
        }

        [Test]
        public async Task GetAccessToken_ReturnAccessToken_Correct()
        {
            await _authorizationManager.SetAuthorizationAsync("1234");

            var expectedValue = _authorizationManager.GetAccessToken();

            Assert.That(expectedValue, Is.EqualTo("1234"));
        }

        [Test]
        public void GetAccessToken_NullReferenceException_InCorrect()
        {
            Assert.Throws<NullReferenceException>(() => _authorizationManager.GetAccessToken());
        }

        public void Init()
        {
            _jsonWebToken = new JsonWebToken
            {
                AccessToken = "1234",
                RefreshToken = "5678",
                Expires = DateTime.UtcNow.AddDays(1)
            };

            A.CallTo(() => _webClient.PostAsync<JsonWebToken, string>(
                A<string>.Ignored, A<string>.Ignored, A<bool>.Ignored)).Returns(_jsonWebToken);
        }
    }
}
