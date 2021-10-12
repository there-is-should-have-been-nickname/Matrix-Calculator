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

        private void ActionsFormButtonMultiplicationByNumber_Click(object sender, RoutedEventArgs e)
        {
            var mulpByNumber = new MultiplicationByNumberWindow();
            mulpByNumber.Show();
            Close();
        }

        private void ActionsFormButtonAdditionButton_Click(object sender, RoutedEventArgs e)
        {
            var add = new AdditionWindow();
            add.Show();
            Close();
        }

        private void ActionsFormButtonMultiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            var mult = new MultiplicationWindow();
            mult.Show();
            Close();
        }

        private void ActionsFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            main.ActivateOperationsButton();
            Close();
        }

        private void ActionsFormButtonDeterminateButton_Click(object sender, RoutedEventArgs e)
        {
            var det = new DeterminantWindow();
            det.Show();
            Close();
        }
    }
}
