using System.Windows;

namespace EmployeeManagement.UI
{
    public partial class AuthorizationResultWindow : Window
    {
        public AuthorizationResultWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            DialogResult = true;
            Owner.Close();
        }
    }
}
