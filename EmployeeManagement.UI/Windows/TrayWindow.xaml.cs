using System.Windows;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class TrayWindow : IWindow
    {
        public TrayWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
