using System.Windows;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI
{
    public partial class AuthorizationWindow : IWindow
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void CloseWindow()
        {
            DialogResult = true;
        }
    }
}
