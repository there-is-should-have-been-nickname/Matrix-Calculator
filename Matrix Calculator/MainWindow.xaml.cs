using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
