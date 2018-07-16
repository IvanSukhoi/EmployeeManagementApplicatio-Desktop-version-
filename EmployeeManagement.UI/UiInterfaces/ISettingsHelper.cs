using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.UI.UiInterfaces
{
    public interface ISettingsHelper
    {
        void SetTheme(SettingsModel settingsModel);
        void SetLanguage(SettingsModel settings);
    }
}
