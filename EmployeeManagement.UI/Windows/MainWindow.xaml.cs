using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.ViewModels;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class MainWindow
    {
        public MainWindow(IMainViewModel mainViewModel, INavigationManager navigationManager)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            navigationManager.SetNavigationService(Frame.NavigationService);
        }

        public async Task InitAsync()
        {
           await ((IMainViewModel)DataContext).InitAsync();
        }
    }
}