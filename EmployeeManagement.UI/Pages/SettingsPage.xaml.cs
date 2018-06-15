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

        public void Init()
        {
            ((SettingsViewModel)DataContext).SetSettings();
        }
    }
}
