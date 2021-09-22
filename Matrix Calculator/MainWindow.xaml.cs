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
        public Matrix m1;
        public Matrix m2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            int[,] mas = new int[5, 5];

            for (int i = 0; i < 5; ++i)
            {
                for (int p = 0; p < 5; ++p)
                {
                    var num = rand.Next(10);
                    mas[i, p] = num;
                }
            }

            m1 = new Matrix(5, 5, mas);
            mas = new int[5, 5];
            for (int i = 0; i < 5; ++i)
            {
                for (int p = 0; p < 5; ++p)
                {
                    var num = rand.Next(10);
                    mas[i, p] = num;
                }
            }

            

            m2 = new Matrix(5, 5, mas);

            string str = "";

            for (int i = 0; i < 5; ++i)
            {
                for (int p = 0; p < 5; ++p)
                {
                    str += m1.nums[i, p].ToString() + " ";
                }
                str += '\n';
            }

            text1.Text = str;

            str = "";
            for (int i = 0; i < 5; ++i)
            {
                for (int p = 0; p < 5; ++p)
                {
                    str += m2.nums[i, p].ToString() + " ";
                }
                str += '\n';
            }

            text2.Text = str;
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            var additionMatrix = m1.multiplication(m2);

            string str = "";

            for (int i = 0; i < 5; ++i)
            {
                for (int p = 0; p < 5; ++p)
                {
                    str += additionMatrix.nums[i, p].ToString() + " ";
                }
                str += '\n';
            }

            text3.Text = str;


            int det1 = m1.getDeterminant();
            int det2 = m2.getDeterminant();

            text4.Text = $"det1 {det1} \t det2 {det2}";
        }
    }
}
