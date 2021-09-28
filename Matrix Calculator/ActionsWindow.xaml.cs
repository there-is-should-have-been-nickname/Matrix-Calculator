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
using System.Windows.Shapes;

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
    }
}
