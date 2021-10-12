using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFormButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainFormButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Show();
            Close();
        }

        private void MainFormButtonActions_Click(object sender, RoutedEventArgs e)
        {
            var actionsWindow = new ActionsWindow();
            actionsWindow.Show();
            Close();
        }

        public void ActivateOperationsButton()
        {
            mainFormButtonActions.IsEnabled = true;
        }
    }
}
