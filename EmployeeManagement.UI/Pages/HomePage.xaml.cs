using System.Threading.Tasks;
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

        public async Task Init()
        {
            await ((HomeViewModel)DataContext).InitAsync();
        }
    }
}
