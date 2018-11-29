using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class SettingsRepositoryTest
    {
        private SettingsRepository _settingsRepository;

        private IWebClient _webClient;
        private IAuthorizationManager _authorizationManager;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _authorizationManager = A.Fake<IAuthorizationManager>();

            _settingsRepository = new SettingsRepository(_webClient, _authorizationManager);
        }

        [Test]
        public async Task GetByIdAsync_ReturnSettingsModel_Correct()
        {
            var settingsModel = new SettingsModel
            {
                Theme = Theme.Dark,
                UserId = 1,
            };

            A.CallTo(() => _webClient.GetAsync<SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings + "1", true))
                .Returns(settingsModel);

            var expectedValue = await _settingsRepository.GetByUserIdAsync(1);

            Assert.IsNotNull(expectedValue);
            Assert.That(expectedValue.Theme, Is.EqualTo(Theme.Dark));
        }

        [Test]
        public void GetByIdAsync_ReturnSettingModel_Correct()
        {
            A.CallTo(() => _webClient.GetAsync<SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings + "-1", true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _settingsRepository.GetByUserIdAsync(-1));
        }

    [Test]
        public void SaveAsync_SaveEmployeeModel_Correct()
        {
            var settingsModel = new SettingsModel();

            A.CallTo(() =>
                _webClient.PostAsync<SettingsModel, SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings,
                    settingsModel, true)).Returns(settingsModel);

            Assert.DoesNotThrowAsync(() => _settingsRepository.SaveAsync(settingsModel));
        }

        [Test]
        public void SaveAsync_InvalidOperatingException_InCorrect()
        {
            var settingsModel = new SettingsModel();

            A.CallTo(() => 
                    _webClient.PostAsync<SettingsModel, SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings, settingsModel, true))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _settingsRepository.SaveAsync(settingsModel));
        }
    }
}
