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

        private WindowService _windowService;

        [SetUp]
        public void SetUp()
        {
            _authorizationService = A.Fake<IAuthorizationService>();
            _windowFactory = A.Fake<IWindowFactory>();
            _settingsHelper = A.Fake<ISettingsHelper>();
            _settingsService = A.Fake<ISettingsService>();
            _windowService = new WindowService(_settingsHelper, _settingsService, _authorizationService, _windowFactory);
        }

        [Test]
        public async Task CreateMainWindowAsync_CreateMainWindow_Correct()
        {
            var initTrigger = false;
            var showTrigger = false;

            A.CallTo(() => _settingsService.GetByUserIdAsync(A<int>.Ignored)).Returns(new SettingsModel { Language = Language.English });
            A.CallTo(() => _authorizationService.GetCurrentUser()).Returns(new UserModel { Id = 10 });

            var mainWindow = A.Fake<MainWindow>();
            A.CallTo(() => mainWindow.InitAsync()).Invokes(() => initTrigger = true);
            A.CallTo(() => mainWindow.ShowWindow()).Invokes(() => showTrigger = true);

            A.CallTo(() => _windowFactory.Create<MainWindow>())
                .Returns(mainWindow);

            await _windowService.CreateMainWindowAsync();

            A.CallTo(() => _settingsHelper.SetLanguage(A<SettingsModel>.Ignored)).MustHaveHappened();
            A.CallTo(() => _settingsHelper.SetTheme(A<SettingsModel>.Ignored)).MustHaveHappened();
            Assert.IsTrue(initTrigger);
            Assert.IsTrue(showTrigger);
        }

        [Test]
        public void CreateAuthorizationWindow_CreateAuthorizationWindow_Correc()
        {
            var window = A.Fake<AuthorizationWindow>();

            A.CallTo(() => _windowFactory.Create<AuthorizationWindow>()).Returns(window);
            A.CallTo(() => window.ShowDialogWindow()).Returns(false);

            _windowService.CreateAuthorizationWindow();

            A.CallTo(() => window.ShowDialogWindow()).MustHaveHappened();
        }
    }
}
