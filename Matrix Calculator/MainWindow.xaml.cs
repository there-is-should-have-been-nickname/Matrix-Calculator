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

        private void mainFormButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mainFormButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new helpWindow();
            helpWindow.Show();
            Close();
        }

        private void mainFormButtonActions_Click(object sender, RoutedEventArgs e)
        {
            var actionsWindow = new ActionsWindow();
            actionsWindow.Show();
            Close();
        }
    }
}
