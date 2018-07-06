using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using EmployeeManagement.UI.Managers;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class MainWindow
    {
        public MainWindow(MainViewModel mainViewModel, NavigationManager navigationManager)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            navigationManager.SetNavigationService(Frame.NavigationService);
        }

        public async Task InitAsync()
        {
           await ((MainViewModel)DataContext).InitAsync();
        }
    }
}