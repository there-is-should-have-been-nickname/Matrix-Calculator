using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для ActionsWindow.xaml
    /// </summary>
    public partial class ActionsWindow : Window
    {
        public ActionsWindow()
        {
            InitializeComponent();
        }

        private void actionsFormButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }

        private void actionsFormButtonMultiplicationByNumber_Click(object sender, RoutedEventArgs e)
        {
            var mulpByNumber = new MultiplicationByNumberWindow();
            mulpByNumber.Show();
            Close();
        }

        private void actionsFormButtonAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            var add = new AdditionWindow();
            add.Show();
            Close();
        }

        private void actionsFormButtonMultiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            var mult = new MultiplicationWindow();
            mult.Show();
            Close();
        }

        private void actionsFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            Close();
        }

        private void actionsFormButtonDeterminateButton_Click(object sender, RoutedEventArgs e)
        {
            var det = new DeterminantWindow();
            det.Show();
            Close();
        }
    }
}
