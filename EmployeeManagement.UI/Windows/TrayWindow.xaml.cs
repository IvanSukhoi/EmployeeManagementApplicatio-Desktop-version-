using System.Threading.Tasks;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class TrayWindow
    {
        public TrayWindow(TrayViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public async Task Init()
        {
            //var t = this.Dispatcher.InvokeAsync(async () => await ((TrayViewModel)DataContext).InitAsync());
            //Task.WaitAll(t.Task);
            //Task.Factory.StartNew(() => ((TrayViewModel) DataContext).InitAsync());

            await ((TrayViewModel) DataContext).InitAsync();
        }
    }
}
