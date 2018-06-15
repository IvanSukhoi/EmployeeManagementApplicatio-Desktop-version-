using System.Windows.Controls;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Pages
{
    public partial class HomePage
    {
        public HomePage(HomeViewModel homeViewModel)
        {
            InitializeComponent();
            DataContext = homeViewModel;
        }

        public void Init()
        {
            ((HomeViewModel)DataContext).Init();
        }
    }
}
