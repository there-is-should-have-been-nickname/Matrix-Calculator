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
    /// Логика взаимодействия для MultiplicationByNumberWindow.xaml
    /// </summary>
    public partial class MultiplicationByNumberWindow : Window
    {
        private int rows { get; set; }
        private int columns { get; set; }

        private Grid innerGrid;

        private Matrix initialMatrix;
        private TextBox[,] initialMatrixTextBox;
        
        private Matrix finalMatrix;
        private TextBox[,] finalMatrixTextBox;
        
        private Button buttonCalculate;     

        private TextBox numberTextBox;
        private int number { get; set; }

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public MultiplicationByNumberWindow()
        {
            InitializeComponent();
        }

        private void multiplicationByNumberFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (canParseRowsAndColumns())
            {
                parseRowsAndColumns();
                creationAndInsertionInnerGrid();
                creationAndInsertionInitialMatrixTextBox();
                creationAndInsertionNumberTextBox();
                creationAndInsertionButtonCalculate();
                


            } else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
            
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            clearInitialMatrixTextBox();
            
            if (canParseInitialMatrixTextBox() && canParseNumberTextBox())
            {
                int[,] initialMatrixMas = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox[i, p].Text);
                        initialMatrixMas[i, p] = elem;
                    }
                }

                number = Convert.ToInt32(numberTextBox.Text);

                initialMatrix = new Matrix(rows, columns, initialMatrixMas);
                finalMatrix = initialMatrix.multiplicationOnNumber(number);
                creationAndInsertionFinalMatrix();
                activateTimer();
            } else
            {
                MessageBox.Show("Число или один (или более) элементов матрицы не являются числом. Пожалуйста, исправьте это и попробуйте снова");
            }
        }

        

        private bool canParseRowsAndColumns()
        {
            bool isParsedRows = int.TryParse(multiplicationByNumberFormRows.Text, out int value);
            bool isParsedColumns = int.TryParse(multiplicationByNumberFormColumns.Text, out int value2);

            if (isParsedRows && isParsedColumns)
            {
                return true;
            }
            return false;
        }

        private void parseRowsAndColumns()
        {
            rows = Convert.ToInt32(multiplicationByNumberFormRows.Text);
            columns = Convert.ToInt32(multiplicationByNumberFormColumns.Text);
        }

        private void creationAndInsertionInnerGrid()
        {
            innerGrid = new Grid();
            innerGrid.Margin = new Thickness(100, 90, 0, 0);
            innerGrid.Width = Width * 0.9;
            innerGrid.Height = Height * 0.73;
            
            //TODO: delete background
            innerGrid.Background = Brushes.Gray;
            innerGrid.HorizontalAlignment = HorizontalAlignment.Left;
            innerGrid.VerticalAlignment = VerticalAlignment.Top;
            multiplicationByNumberFormGrid.Children.Add(innerGrid);
        }

        private void clearInitialMatrixTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    initialMatrixTextBox[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox[i, p].Tag = 0;
                    initialMatrixTextBox[i, p].Name = "";
                }
            }
        }

        private void creationAndInsertionInitialMatrixTextBox()
        {
            initialMatrixTextBox = new TextBox[rows, columns];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    var elemTextBox = new TextBox();
                    //
                    elemTextBox.FontFamily = new FontFamily("Consolas");
                    elemTextBox.FontSize = 18;
                    elemTextBox.Width = 40;
                    elemTextBox.Height = 40;
                    elemTextBox.Text = "";
                    elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;

                    elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                    elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                    elemTextBox.Margin = new Thickness(20 + p * 50, 20 + i * 50, 0, 0);
                    initialMatrixTextBox[i, p] = elemTextBox;
                    
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox();
        }

        private void creationAndInsertionNumberTextBox()
        {
            var operatorLabel = new Label();
            operatorLabel.FontFamily = new FontFamily("Consolas");
            operatorLabel.FontSize = 14;
            
            operatorLabel.Width = 30;
            operatorLabel.Height = 30;
            operatorLabel.Content = "X";
            operatorLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorLabel.VerticalContentAlignment = VerticalAlignment.Center;


            operatorLabel.VerticalAlignment = VerticalAlignment.Top;
            operatorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            operatorLabel.Margin = new Thickness(columns * 50 + 50, rows * 50 / 2, 0, 0);
            innerGrid.Children.Add(operatorLabel);

            numberTextBox = new TextBox();
            numberTextBox.FontFamily = new FontFamily("Consolas");
            numberTextBox.FontSize = 18;
            numberTextBox.Width = 40;
            numberTextBox.Height = 40;
            numberTextBox.Text = "";
            numberTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            numberTextBox.VerticalContentAlignment = VerticalAlignment.Center;

            numberTextBox.Background = Brushes.Gray;

            numberTextBox.VerticalAlignment = VerticalAlignment.Top;
            numberTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            numberTextBox.Margin = new Thickness(columns * 50 + 100, rows * 50 / 2 - 5, 0, 0);
            innerGrid.Children.Add(numberTextBox);

            var operatorEqual = new Label();
            operatorEqual.FontFamily = new FontFamily("Consolas");
            operatorEqual.FontSize = 14;

            operatorEqual.Width = 30;
            operatorEqual.Height = 30;
            operatorEqual.Content = "=";
            operatorEqual.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorEqual.VerticalContentAlignment = VerticalAlignment.Center;


            operatorEqual.VerticalAlignment = VerticalAlignment.Top;
            operatorEqual.HorizontalAlignment = HorizontalAlignment.Left;
            operatorEqual.Margin = new Thickness(columns * 50 + 150, rows * 50 / 2, 0, 0);
            innerGrid.Children.Add(operatorEqual);
        }

        private void creationAndInsertionButtonCalculate()
        {
            var brushConverter = new BrushConverter();
           
            buttonCalculate = new Button();
            buttonCalculate.FontFamily = new FontFamily("Consolas");
            buttonCalculate.FontSize = 14;
            buttonCalculate.Content = "Посчитать";
            buttonCalculate.Width = 138;
            buttonCalculate.Height = 30;
            buttonCalculate.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF2B16B4");
            buttonCalculate.BorderThickness = new Thickness(2, 2, 2, 2);
            buttonCalculate.HorizontalAlignment = HorizontalAlignment.Left;
            buttonCalculate.VerticalAlignment = VerticalAlignment.Top;

            buttonCalculate.Background = (Brush)brushConverter.ConvertFrom("#FFABB2AF");

            buttonCalculate.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonCalculate.VerticalContentAlignment = VerticalAlignment.Center;
            buttonCalculate.Margin = new Thickness(800, 40, 0, 0);
            buttonCalculate.Click += buttonCalculate_Click;
            multiplicationByNumberFormGrid.Children.Add(buttonCalculate);
        }

        private void creationAndInsertionFinalMatrix()
        {
            finalMatrixTextBox = new TextBox[rows, columns];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    var elemTextBox = new TextBox();
                    //
                    elemTextBox.FontFamily = new FontFamily("Consolas");
                    elemTextBox.FontSize = 18;
                    elemTextBox.Width = 40;
                    elemTextBox.Height = 40;
                    elemTextBox.Text = "";
                    elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;
                    elemTextBox.Background = Brushes.Gray;

                    elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                    elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                    elemTextBox.Margin = new Thickness(20 + p * 50 + (columns * 50 + 150 + 50), 20 + i * 50, 0, 0);
                    finalMatrixTextBox[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
        }

        private bool canParseInitialMatrixTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox[i,p].Text, out int value);

                    if (!isParsed)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        private bool canParseNumberTextBox()
        {
            bool isParsed = int.TryParse(numberTextBox.Text, out int value);

            if (!isParsed)
            {
                return false;
            }
            return true;
        }

        private void activateTimer()
        {
            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {

            if (isAllMatrixCalculated())
            {
                initialMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                finalMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                numberTextBox.Background = Brushes.Gray;
                timer.Stop();
                timer.Tick -= timerTick;
            }
            else
            {
                fillNextTextBox();
            }

        }

        private void fillNextTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) == 0)
                    {
                        initialMatrixTextBox[i, p].Background = Brushes.Green;
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                        numberTextBox.Background = Brushes.Coral;
                        finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                        initialMatrixTextBox[i, p].Tag = 1;
                        return;
                    }
                    initialMatrixTextBox[i, p].Background = Brushes.Gray;
                    finalMatrixTextBox[i, p].Background = Brushes.Gray;

                }

            }
        }

        private bool isAllMatrixCalculated()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i,p].Tag) == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        
    }
}
