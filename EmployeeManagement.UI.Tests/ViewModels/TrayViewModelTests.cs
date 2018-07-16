using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    [Apartment(ApartmentState.STA)]
    public class TrayViewModelTests
    {
        private IAuthorizationService _authorizationService;
        private IWindowFactory _windowFactory;
        private IApplicationService _applicationService;
        private IWindowService _windowService;
        private TrayViewModel _trayViewModel;

        [SetUp]
        public void SetUp()
        {
            _authorizationService = A.Fake<IAuthorizationService>();
            _windowFactory = A.Fake<IWindowFactory>();
            _applicationService = A.Fake<IApplicationService>();
            _windowService = A.Fake<IWindowService>();
            _trayViewModel = new TrayViewModel(_authorizationService, _windowFactory, _applicationService, _windowService);
        }

        [Test]
        public async Task InitAsync_CreateMainWindow_Correct()
        {
            A.CallTo(() => _authorizationService.IsAuthorized()).Returns(true);

            await _trayViewModel.InitAsync();

            A.CallTo(() => _windowService.CreateMainWindowAsync()).MustHaveHappened();
        }

        [Test]
        public async Task InitAsync_CreateAuthorizationWindow_Correct()
        {
            await _trayViewModel.InitAsync();

            A.CallTo(() => _windowService.CreateAuthorizationWindow()).MustHaveHappened();
        }

        [Test]
        public async Task ExecuteTransitionToMainAsync_CreateMainWindow_Correct()
        {
            await _trayViewModel.ExecuteTransitionToMainAsync(new object());

            A.CallTo(() => _windowService.CreateMainWindowAsync()).MustHaveHappened();
        }

        [Test]
        public void ExecuteTransitionToExit_CloseAllWindows_Correct()
        {
            _trayViewModel.ExecuteTransitionToExit(new object());

            A.CallTo(() => _applicationService.Shutdown()).MustHaveHappened();
        }

        [Test]
        public void ExecuteTransitionToAuthorization_CreateAuthorizationWindow_Correct()
        {
            A.CallTo(() => _authorizationService.IsLogged).Returns(false);

            _trayViewModel.ExecuteTransitionToAuthorization(new object());

            A.CallTo(() => _windowService.CreateAuthorizationWindow()).MustHaveHappened();
        }

        [Test]
        public void ExecuteTransitionToAuthorization_CloseMainWindow_Correct()
        {
            A.CallTo(() => _authorizationService.IsLogged).Returns(true);

            _trayViewModel.ExecuteTransitionToAuthorization(new object());

            A.CallTo(() => _windowFactory.Close<MainWindow>()).MustHaveHappened();
            A.CallTo(() => _authorizationService.LogOut()).MustHaveHappened();
        }
    }
}
