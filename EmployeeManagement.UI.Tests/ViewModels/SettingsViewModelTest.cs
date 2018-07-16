using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.ViewModels;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    [Apartment(ApartmentState.STA)]
    public class SettingsViewModelTest
    {
        private ISettingsService _settingsService;
        private IAuthorizationService _authorizationService;
        private IWindowFactory _windowFactory;
        private ISettingsHelper _settingsHelper;
        private SettingsViewModel _settingsViewModel;
        private INavigationManager _navigationManager;
        private IMainViewModel _mainViewModel;

        [SetUp]
        public void SetUp()
        {
            _settingsService = A.Fake<ISettingsService>();
            _authorizationService = A.Fake<IAuthorizationService>();
            _windowFactory = A.Fake<IWindowFactory>();
            _settingsHelper = A.Fake<ISettingsHelper>();
            _navigationManager = A.Fake<INavigationManager>();
            _mainViewModel = A.Fake<IMainViewModel>();
            _settingsViewModel = new SettingsViewModel(_settingsService, _authorizationService, _windowFactory, _settingsHelper);
        }

        [Test]
        public async Task ExecuteSelectThemeAsync_SelectTheme_Correct()
        {
            _settingsViewModel.SettingsModel = new SettingsModel {Language = Language.Russian, Theme = Theme.Ligth};

            await _settingsViewModel.ExecuteSelectThemeAsync(Theme.Dark);

            A.CallTo(() => _settingsHelper.SetTheme(_settingsViewModel.SettingsModel)).MustHaveHappened();
            A.CallTo(() => _settingsService.SaveAsync(_settingsViewModel.SettingsModel)).MustHaveHappened();
            Assert.That(_settingsViewModel.SettingsModel.Theme, Is.EqualTo(Theme.Dark));
        }

        [Test]
        public async Task ExecuteSelectLanguageAsync_SelectLanguage_Correct()
        {
            _settingsViewModel.SettingsModel = new SettingsModel{Language = Language.Russian, Theme = Theme.Ligth};

            await _settingsViewModel.ExecuteSelectLanguageAsync(Language.English);

            A.CallTo(() => _settingsService.SaveAsync(_settingsViewModel.SettingsModel)).MustHaveHappened();
            Assert.IsTrue(_settingsViewModel.IsEditingLanguage);
        }

        [Test]
        public void BackToCurrentLanguage_AssignCurrentLanguage_Correct()
        {
            _settingsViewModel.SettingsModel = new SettingsModel{Language = Language.English};
            _settingsViewModel.CurrentLanguage = Language.Russian;

            _settingsViewModel.BackToCurrentLanguage(new object());

            Assert.That(_settingsViewModel.SettingsModel.Language, Is.EqualTo(Language.Russian));
            Assert.IsFalse(_settingsViewModel.IsEditingLanguage);
        }

        [Test]
        public async Task ExecuteRestartMainWindowAsync_RestartMainWindow_Correct()
        {
            A.CallTo(() => _settingsService.GetByUserIdAsync(A<int>.Ignored)).Returns(new SettingsModel { Language = Language.English });
            A.CallTo(() => _authorizationService.GetCurrentUser()).Returns(new UserModel { Id = 10 });

            var mainWindow = new MainWindow(_mainViewModel, _navigationManager);

            A.CallTo(() => _windowFactory.Create<MainWindow>()).Returns(mainWindow);

            await _settingsViewModel.ExecuteRestartMainWindowAsync(new object());

            A.CallTo(() => _settingsHelper.SetLanguage(A<SettingsModel>.Ignored)).MustHaveHappened();
            A.CallTo(() => _windowFactory.Close<MainWindow>()).MustHaveHappened();
            A.CallTo(() => _windowFactory.Create<MainWindow>()).MustHaveHappened();
        }
    }
}
