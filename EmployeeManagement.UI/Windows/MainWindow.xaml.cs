using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {}

        public MainWindow(MainViewModel mainViewModel/*, INavigationManager navigationManager*/)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            //navigationManager.SetNavigationService(Frame.NavigationService);
        }

        public virtual async Task InitAsync()
        {
           await ((MainViewModel)DataContext).InitAsync();
        }

        public virtual void ShowWindow()
        {
            Show();
        }
    }
}