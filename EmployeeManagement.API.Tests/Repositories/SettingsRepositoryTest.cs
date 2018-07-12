using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.API.Tests.Repositories
{
    public class SettingsRepositoryTest
    {
        private SettingsRepository _settingsRepository;
        private IWebClient _webClient;

        [SetUp]
        public void SetUp()
        {
            _webClient = A.Fake<IWebClient>();
            _settingsRepository = new SettingsRepository(_webClient);
        }

        [Test]
        public async Task GetByIdAsync_ReturnSettingsModel_Correct()
        {
            var settingsModel = new SettingsModel
            {
                Theme = Theme.Dark,
                UserId = 1,
                Language = Language.English,
                UserModel = new UserModel()
            };

            A.CallTo(() => _webClient.GetAsync<SettingsModel>(SettingsConfiguration.ApiUrls.Settings.GetById + "1"))
                .Returns(settingsModel);

            var expectedValue = await _settingsRepository.GetByUserIdAsync(1);

            AssertPropertyValue(expectedValue, settingsModel);
        }

        [Test]
        public void GetByIdAsync_ReturnSettingModel_Correct()
        {
            A.CallTo(() => _webClient.GetAsync<SettingsModel>(SettingsConfiguration.ApiUrls.Settings.GetById + "-1"))
                .Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _settingsRepository.GetByUserIdAsync(-1));
        }

    [Test]
        public void SaveAsync_SaveEmployeeModel_Correct()
        {
            var settingsModel = new SettingsModel();

            A.CallTo(() =>
                _webClient.PostAsync<SettingsModel, SettingsModel>(SettingsConfiguration.ApiUrls.Settings.Save,
                    settingsModel)).Returns(settingsModel);

            Assert.DoesNotThrowAsync(() => _settingsRepository.SaveAsync(settingsModel));
        }

        [Test]
        public void SaveAsync_InvalidOperatingException_InCorrect()
        {
            var settingsModel = new SettingsModel();

            A.CallTo(() => _webClient.PostAsync<SettingsModel, SettingsModel>(SettingsConfiguration.ApiUrls.Settings.Save, settingsModel)).Throws<InvalidOperationException>();

            Assert.ThrowsAsync<InvalidOperationException>(() => _settingsRepository.SaveAsync(settingsModel));
        }

        public void AssertPropertyValue(SettingsModel expectedValue, SettingsModel settingsModel)
        {
            Assert.That(expectedValue.UserId, Is.EqualTo(settingsModel.UserId));
            Assert.That(expectedValue.Theme, Is.EqualTo(settingsModel.Theme));
            Assert.That(expectedValue.Language, Is.EqualTo(settingsModel.Language));
        }
    }
}
