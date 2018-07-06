using System.Threading.Tasks;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Pages
{
    public partial class SettingsPage
    {
        public SettingsPage(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            DataContext = settingsViewModel;
        }

        public async Task InitAsync()
        {
            await ((SettingsViewModel) DataContext).SetSettings();
        }
    }
}
