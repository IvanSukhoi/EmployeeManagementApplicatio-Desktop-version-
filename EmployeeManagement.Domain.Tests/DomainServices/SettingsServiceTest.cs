using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class SettingsServiceTest
    {
        private ISettingsRepository _settingsRepository;
        private List<SettingsModel> _settingsModels;

        [SetUp]
        public void SetUp()
        {
            _settingsRepository = A.Fake<ISettingsRepository>();
            InitRepository();
        }

        [Test]
        public async Task GetByUserIdAsync_ReturnUserModel_Correct()
        {
            var expectedValue = await _settingsRepository.GetByUserIdAsync(1);

            AssertPropertyValue(expectedValue, _settingsModels.FirstOrDefault(x => x.UserId == 1));
        }

        [Test]
        public async Task SaveAsync_SaveSettings_Correct()
        {
            var settingsModel = new SettingsModel
            {
                UserId = 37,
                Language = Language.Russian,
                Theme = Theme.Ligth,
                UserModel = new UserModel()
            };

            await _settingsRepository.SaveAsync(settingsModel);

            AssertPropertyValue(_settingsModels.FirstOrDefault(x => x.UserId == 37), settingsModel);
        }

        public void AssertPropertyValue(SettingsModel expectedValue, SettingsModel settingsModel)
        {
            Assert.That(expectedValue.UserId, Is.EqualTo(settingsModel.UserId));
            Assert.That(expectedValue.Theme, Is.EqualTo(settingsModel.Theme));
            Assert.That(expectedValue.Language, Is.EqualTo(settingsModel.Language));
        }

        public void InitRepository()
        {
            _settingsModels = new List<SettingsModel>
            {
                new SettingsModel
                {
                    UserId = 1,
                    Language = Language.English,
                    Theme = Theme.Dark,
                    UserModel = new UserModel()
                },
                new SettingsModel
                {
                    UserId = 2,
                    Language = Language.Russian,
                    Theme = Theme.Dark,
                    UserModel = new UserModel()
                }
            };

            A.CallTo(() => _settingsRepository.GetByUserIdAsync(A<int>.Ignored)).ReturnsLazily((int userId) =>
                _settingsModels.FirstOrDefault(x => x.UserId == userId));

            A.CallTo(() => _settingsRepository.SaveAsync(A<SettingsModel>.Ignored)).Invokes(
                (SettingsModel settingsModel) =>
                {
                    _settingsModels.RemoveAll(x => x.UserId == settingsModel.UserId);
                    _settingsModels.Add(settingsModel);
                });
        }
    }
}
