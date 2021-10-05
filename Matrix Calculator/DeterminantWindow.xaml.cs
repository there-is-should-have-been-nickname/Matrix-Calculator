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
    /// Логика взаимодействия для DeterminantWindow.xaml
    /// </summary>
    public partial class DeterminantWindow : Window
    {
        private Assistant assistant;
        public DeterminantWindow()
        {
            InitializeComponent();
            assistant = new Assistant(4, determinantFormButtonCreate, determinantFormButtonCalculate,
                determinantFormButtonBack, determinantFormInitialMatrixColor);
        }

        private void determinantFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (assistant.canParseOrder(determinantFormOrder))
            {
                assistant.parseOrder(determinantFormOrder);
                assistant.enableColorsAndSpeed(determinantFormInitialMatrixColor,
                    determinantFormSpeedLight, determinantFormButtonCalculate);
                assistant.setHeightWindow(this);
                assistant.creationAndInsertionInnerGrid(determinantFormGrid, this);
                assistant.creationAndInsertionInitialMatrixTextBox();
                assistant.creationAndInsertionDeterminantTextBox();
            }
            else
            {
                MessageBox.Show("Вы не выбрали порядок матрицы. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
        }

        private void determinantFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox();

            if (assistant.canParseInitialMatrixTextBox() 
                && assistant.canDefineColors(determinantFormInitialMatrixColor))
            {
                assistant.initialMatrix = assistant.getInitialMatrix();
                assistant.determinant = assistant.initialMatrix.getDeterminant();
                assistant.activateTimer(determinantFormButtonCreate, determinantFormButtonCalculate,
                    determinantFormButtonBack, determinantFormSpeedLightLabel);
            }
            else
            {
                MessageBox.Show("Один (или более) элементов матрицы не являются числом. Или вы не указали цвет подсветки. Пожалуйста, исправьте это и попробуйте снова");
            }
        }



        //private bool canParseOrder()
        //{
        //    bool isParsedOrder = int.TryParse(determinantFormOrder.Text, out int value);
            
        //    if (isParsedOrder)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private void parseOrder()
        //{
        //    rows = Convert.ToInt32(determinantFormOrder.Text);
        //    columns = rows;
        //}

        //private void creationAndInsertionInnerGrid()
        //{
        //    innerGrid = new Grid();
        //    innerGrid.Margin = new Thickness(100, 90, 0, 0);
        //    innerGrid.Width = Width * 0.9;
        //    innerGrid.Height = Height * 0.73;

        //    //TODO: delete background
        //    innerGrid.Background = Brushes.Gray;
        //    innerGrid.HorizontalAlignment = HorizontalAlignment.Left;
        //    innerGrid.VerticalAlignment = VerticalAlignment.Top;
        //    determinantFormGrid.Children.Add(innerGrid);
        //}

        //private void clearInitialMatrixTextBox()
        //{
        //    for (int i = 0; i < rows; ++i)
        //    {
        //        for (int p = 0; p < columns; ++p)
        //        {
        //            initialMatrixTextBox[i, p].Background = Brushes.Gray;
        //            initialMatrixTextBox[i, p].Tag = 0;
        //            initialMatrixTextBox[i, p].Name = "";
        //        }
        //    }
        //}

        //private void creationAndInsertionInitialMatrixTextBox()
        //{
        //    initialMatrixTextBox = new TextBox[rows, columns];

        //    for (int i = 0; i < rows; ++i)
        //    {
        //        for (int p = 0; p < columns; ++p)
        //        {
        //            var elemTextBox = new TextBox();
        //            //
        //            elemTextBox.FontFamily = new FontFamily("Consolas");
        //            elemTextBox.FontSize = 18;
        //            elemTextBox.Width = 40;
        //            elemTextBox.Height = 40;
        //            elemTextBox.Text = "";
        //            elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
        //            elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;

        //            elemTextBox.VerticalAlignment = VerticalAlignment.Top;
        //            elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
        //            elemTextBox.Margin = new Thickness(20 + p * 50, 20 + i * 50, 0, 0);
        //            initialMatrixTextBox[i, p] = elemTextBox;

        //            innerGrid.Children.Add(elemTextBox);
        //        }
        //    }
        //    clearInitialMatrixTextBox();
        //}
        //private void creationAndInsertionDeterminantTextBox()
        //{
        //    determinantTextBox = new TextBox();
        //    determinantTextBox.FontFamily = new FontFamily("Consolas");
        //    determinantTextBox.FontSize = 18;
        //    determinantTextBox.Width = 40;
        //    determinantTextBox.Height = 40;
        //    determinantTextBox.Text = "";
        //    determinantTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
        //    determinantTextBox.VerticalContentAlignment = VerticalAlignment.Center;

        //    determinantTextBox.Background = Brushes.Gray;

        //    determinantTextBox.VerticalAlignment = VerticalAlignment.Top;
        //    determinantTextBox.HorizontalAlignment = HorizontalAlignment.Left;
        //    determinantTextBox.Margin = new Thickness(columns * 50 + 100, rows * 50 / 2 - 5, 0, 0);
        //    innerGrid.Children.Add(determinantTextBox);

        //}

        //private void creationAndInsertionButtonCalculate()
        //{
        //    var brushConverter = new BrushConverter();

        //    buttonCalculate = new Button();
        //    buttonCalculate.FontFamily = new FontFamily("Consolas");
        //    buttonCalculate.FontSize = 14;
        //    buttonCalculate.Content = "Посчитать";
        //    buttonCalculate.Width = 138;
        //    buttonCalculate.Height = 30;
        //    buttonCalculate.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF2B16B4");
        //    buttonCalculate.BorderThickness = new Thickness(2, 2, 2, 2);
        //    buttonCalculate.HorizontalAlignment = HorizontalAlignment.Left;
        //    buttonCalculate.VerticalAlignment = VerticalAlignment.Top;

        //    buttonCalculate.Background = (Brush)brushConverter.ConvertFrom("#FFABB2AF");

        //    buttonCalculate.HorizontalContentAlignment = HorizontalAlignment.Center;
        //    buttonCalculate.VerticalContentAlignment = VerticalAlignment.Center;
        //    buttonCalculate.Margin = new Thickness(530, 40, 0, 0);
        //    buttonCalculate.Click += buttonCalculate_Click;
        //    determinantFormGrid.Children.Add(buttonCalculate);
        //}

        
        //private bool canParseInitialMatrixTextBox()
        //{
        //    for (int i = 0; i < rows; ++i)
        //    {
        //        for (int p = 0; p < columns; ++p)
        //        {
        //            bool isParsed = int.TryParse(initialMatrixTextBox[i, p].Text, out int value);

        //            if (!isParsed)
        //            {
        //                return false;
        //            }
        //        }

        //    }

        //    return true;
        //}

        //private void activateTimer()
        //{
        //    timer.Tick += timerTick;
        //    timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        //    timer.Start();
        //}

        //private void timerTick(object sender, EventArgs e)
        //{

        //    if (isAllMatrixCalculated())
        //    {
        //        initialMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
        //        determinantTextBox.Text = determinant.ToString();
        //        timer.Stop();
        //        timer.Tick -= timerTick;
        //    }
        //    else
        //    {
        //        fillNextTextBox(rows);
        //    }

        //}

        //private void fillNextTextBox(int order)
        //{
        //    if (order == 1)
        //    {
        //        initialMatrixTextBox[0, 0].Background = Brushes.Green;
        //        initialMatrixTextBox[0, 0].Tag = 2;
        //        return;
        //    } else if (order == 2) {
        //        for (int i = 0; i < rows; ++i)
        //        {
        //            if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
        //            {
        //                initialMatrixTextBox[i, i].Background = Brushes.Green;
        //                initialMatrixTextBox[i, i].Tag = 2;
        //                return;
        //            }
        //            initialMatrixTextBox[i, i].Background = Brushes.Gray;

        //        }

        //        for (int i = 0; i < rows; ++i)
        //        {
        //            if (Convert.ToInt32(initialMatrixTextBox[i, rows - 1 - i].Tag) == 0)
        //            {
        //                initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Green;
        //                initialMatrixTextBox[i, rows - 1 - i].Tag = 2;
        //                return;
        //            }
        //            initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Gray;

        //        }
        //    } else if (order == 3)
        //    {
        //        for (int i = 0; i < rows; ++i)
        //        {
        //            if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
        //            {
        //                initialMatrixTextBox[i, i].Background = Brushes.Green;
        //                initialMatrixTextBox[i, i].Tag = 1;
        //                return;
        //            }
        //            initialMatrixTextBox[i, i].Background = Brushes.Gray;

        //        }

        //        if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 0)
        //        {
        //            initialMatrixTextBox[1, 0].Background = Brushes.Green;
        //            initialMatrixTextBox[1, 0].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[1, 0].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 0)
        //        {
        //            initialMatrixTextBox[2, 1].Background = Brushes.Green;
        //            initialMatrixTextBox[2, 1].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[2, 1].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[0, 2].Tag) == 0)
        //        {
        //            initialMatrixTextBox[0, 2].Background = Brushes.Green;
        //            initialMatrixTextBox[0, 2].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[0, 2].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 0)
        //        {
        //            initialMatrixTextBox[0, 1].Background = Brushes.Green;
        //            initialMatrixTextBox[0, 1].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[0, 1].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 0)
        //        {
        //            initialMatrixTextBox[1, 2].Background = Brushes.Green;
        //            initialMatrixTextBox[1, 2].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[1, 2].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[2, 0].Tag) == 0)
        //        {
        //            initialMatrixTextBox[2, 0].Background = Brushes.Green;
        //            initialMatrixTextBox[2, 0].Tag = 1;
        //            return;
        //        }
        //        initialMatrixTextBox[2, 0].Background = Brushes.Gray;

        //        for (int i = 0; i < rows; ++i)
        //        {
        //            if (Convert.ToInt32(initialMatrixTextBox[i, rows - 1 - i].Tag) == 1)
        //            {
        //                initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Green;
        //                initialMatrixTextBox[i, rows - 1 - i].Tag = 2;
        //                return;
        //            }
        //            initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Gray;

        //        }

        //        if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 1)
        //        {
        //            initialMatrixTextBox[1, 2].Background = Brushes.Green;
        //            initialMatrixTextBox[1, 2].Tag = 2;
        //            return;
        //        }
        //        initialMatrixTextBox[1, 2].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 1)
        //        {
        //            initialMatrixTextBox[2, 1].Background = Brushes.Green;
        //            initialMatrixTextBox[2, 1].Tag = 2;
        //            return;
        //        }
        //        initialMatrixTextBox[2, 1].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[0, 0].Tag) == 1)
        //        {
        //            initialMatrixTextBox[0, 0].Background = Brushes.Green;
        //            initialMatrixTextBox[0, 0].Tag = 2;
        //            return;
        //        }
        //        initialMatrixTextBox[0, 0].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 1)
        //        {
        //            initialMatrixTextBox[1, 0].Background = Brushes.Green;
        //            initialMatrixTextBox[1, 0].Tag = 2;
        //            return;
        //        }
        //        initialMatrixTextBox[1, 0].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 1)
        //        {
        //            initialMatrixTextBox[0, 1].Background = Brushes.Green;
        //            initialMatrixTextBox[0, 1].Tag = 2;
        //            return;
        //        }
        //        initialMatrixTextBox[0, 1].Background = Brushes.Gray;

        //        if (Convert.ToInt32(initialMatrixTextBox[2, 2].Tag) == 1)
        //        {
        //            initialMatrixTextBox[2, 2].Background = Brushes.Green;
        //            initialMatrixTextBox[2, 2].Tag = 2;
        //            return;
        //        }
        //    }
            
        //}

        //private bool isAllMatrixCalculated()
        //{
        //    for (int i = 0; i < rows; ++i)
        //    {
        //        for (int p = 0; p < columns; ++p)
        //        {
        //            if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) < 2)
        //            {
        //                return false;
        //            }
        //        }

        //    }

        //    return true;
        //}

        private void determinantFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            determinantFormSpeedLightLabel.Content = Math.Round(determinantFormSpeedLight.Value);
        }

        private void determinantFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var actions = new ActionsWindow();
            actions.Show();
            Close();
        }
    }
}
