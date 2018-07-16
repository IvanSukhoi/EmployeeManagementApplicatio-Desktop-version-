using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Services;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.ViewModels;
using EmployeeManagement.UI.Windows;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.Services
{
    [Apartment(ApartmentState.STA)]
    public class WindowServiceTest
    {
        private ISettingsHelper _settingsHelper;
        private ISettingsService _settingsService;
        private IAuthorizationService _authorizationService;
        private IWindowFactory _windowFactory;
        private INavigationManager _navigationManager;

        private IMainViewModel _mainViewModel;
        private IAuthorizationViewModel _authorizationViewModel;

        private WindowService _windowService;

        [SetUp]
        public void SetUp()
        {
            _authorizationService = A.Fake<IAuthorizationService>();
            _windowFactory = A.Fake<IWindowFactory>();
            _settingsHelper = A.Fake<ISettingsHelper>();
            _settingsService = A.Fake<ISettingsService>();
            _navigationManager = A.Fake<INavigationManager>();
            _mainViewModel = A.Fake<IMainViewModel>();
            _authorizationViewModel = A.Fake<IAuthorizationViewModel>();
            _windowService = new WindowService(_settingsHelper, _settingsService, _authorizationService, _windowFactory);
        }

        [Test]
        public async Task CreateMainWindowAsync_CreateMainWindow_Correct()
        {
            A.CallTo(() => _settingsService.GetByUserIdAsync(A<int>.Ignored)).Returns(new SettingsModel { Language = Language.English });
            A.CallTo(() => _authorizationService.GetCurrentUser()).Returns(new UserModel { Id = 10 });

            A.CallTo(() => _windowFactory.Create<MainWindow>())
                .Returns(new MainWindow(_mainViewModel, _navigationManager));

            await _windowService.CreateMainWindowAsync();

            A.CallTo(() => _settingsHelper.SetLanguage(A<SettingsModel>.Ignored)).MustHaveHappened();
            A.CallTo(() => _settingsHelper.SetTheme(A<SettingsModel>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void CreateAuthorizationWindow_CreateAuthorizationWindow_Correc()
        {
            A.CallTo(() => _windowFactory.Create<AuthorizationWindow>())
                .Returns(new AuthorizationWindow(_authorizationViewModel));

            _windowService.CreateAuthorizationWindow();

            A.CallTo(() => _windowFactory.Create<AuthorizationWindow>()).MustHaveHappened();
        }
    }
}
