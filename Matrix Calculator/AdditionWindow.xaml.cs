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
    /// Логика взаимодействия для AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {

        private int rows { get; set; }
        private int columns { get; set; }

        private Grid innerGrid;

        private Matrix initialMatrix1;
        private TextBox[,] initialMatrixTextBox1;

        private Matrix initialMatrix2;
        private TextBox[,] initialMatrixTextBox2;

        private Matrix finalMatrix;
        private TextBox[,] finalMatrixTextBox;

        private Button buttonCalculate;

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public AdditionWindow()
        {
            InitializeComponent();
        }

        private void additionFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (canParseRowsAndColumns())
            {
                parseRowsAndColumns();
                creationAndInsertionInnerGrid();
                creationAndInsertionInitialMatrixesTextBox();
                creationAndInsertionButtonCalculate();
            }
            else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            clearInitialMatrixTextBox1();

            if (canParseInitialMatrixesTextBox() && canParseOperator())
            {
                creationAndInsertionOperators();

                int[,] initialMatrixMas1 = new int[rows, columns];
                int[,] initialMatrixMas2 = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        int elem1 = Convert.ToInt32(initialMatrixTextBox1[i, p].Text);
                        initialMatrixMas1[i, p] = elem1;
                        int elem2 = Convert.ToInt32(initialMatrixTextBox2[i, p].Text);
                        initialMatrixMas2[i, p] = elem2;
                                            }
                }


                initialMatrix1 = new Matrix(rows, columns, initialMatrixMas1);
                initialMatrix2 = new Matrix(rows, columns, initialMatrixMas2);
                if (additionFormOperator.Text == "+")
                {
                    finalMatrix = initialMatrix1.addition(initialMatrix2);
                } else
                {
                    finalMatrix = initialMatrix1.substraction(initialMatrix2);
                }
                
                creationAndInsertionFinalMatrix();
                activateTimer();
            }
            else
            {
                MessageBox.Show("Число или один (или более) элементов матрицы не являются числом. Возможно, вы не выбрали оператор, который будет применен к матрицам. Пожалуйста, исправьте это и попробуйте снова");
            }
        }



        private bool canParseRowsAndColumns()
        {
            bool isParsedRows = int.TryParse(additionFormRows.Text, out int value);
            bool isParsedColumns = int.TryParse(additionFormColumns.Text, out int value2);

            if (isParsedRows && isParsedColumns)
            {
                return true;
            }
            return false;
        }

        private void parseRowsAndColumns()
        {
            rows = Convert.ToInt32(additionFormRows.Text);
            columns = Convert.ToInt32(additionFormColumns.Text);
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
            additionFormGrid.Children.Add(innerGrid);
        }

        private void clearInitialMatrixTextBox1()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox1[i, p].Tag = 0;
                }
            }
        }

        private void clearInitialMatrixTextBox2()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    initialMatrixTextBox2[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox2[i, p].Tag = 0;
                }
            }
        }

        private void creationAndInsertionInitialMatrixesTextBox()
        {
            initialMatrixTextBox1 = new TextBox[rows, columns];
            initialMatrixTextBox2 = new TextBox[rows, columns];

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
                    initialMatrixTextBox1[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox1();

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
                    elemTextBox.Margin = new Thickness(20 + p * 50 + columns * 50 + 100, 20 + i * 50, 0, 0);
                    initialMatrixTextBox2[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox2();
        }

        private void creationAndInsertionOperators()
        {
            var operatorLabel = new Label();
            operatorLabel.FontFamily = new FontFamily("Consolas");
            operatorLabel.FontSize = 14;

            operatorLabel.Width = 30;
            operatorLabel.Height = 30;
            operatorLabel.Content = additionFormOperator.Text;
            operatorLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorLabel.VerticalContentAlignment = VerticalAlignment.Center;

            operatorLabel.VerticalAlignment = VerticalAlignment.Top;
            operatorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            operatorLabel.Margin = new Thickness(columns * 50 + 50, rows * 50 / 2, 0, 0);
            innerGrid.Children.Add(operatorLabel);


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
            operatorEqual.Margin = new Thickness((20 + columns * 50) / 2, rows * 50 + 50, 0, 0);
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
            additionFormGrid.Children.Add(buttonCalculate);
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
                    elemTextBox.Margin = new Thickness(20 + p * 50 + columns * 50, 20 + i * 50 + rows * 50, 0, 0);
                    finalMatrixTextBox[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
        }

        private bool canParseOperator()
        {
            if (string.IsNullOrWhiteSpace(additionFormOperator.Text))
            {
                return false;
            }
            return true;
        }

        private bool canParseInitialMatrixesTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox1[i, p].Text, out int value);

                    if (!isParsed)
                    {
                        return false;
                    }
                }

            }

            return true;
        }


        private void activateTimer()
        {
            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {

            if (isAllMatrixCalculated())
            {
                initialMatrixTextBox1[rows - 1, columns - 1].Background = Brushes.Gray;
                initialMatrixTextBox2[rows - 1, columns - 1].Background = Brushes.Gray;
                finalMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
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
                    if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                    {
                        initialMatrixTextBox1[i, p].Background = Brushes.Green;
                        initialMatrixTextBox2[i, p].Background = Brushes.Coral;
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                        finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                        initialMatrixTextBox1[i, p].Tag = 1;
                        return;
                    }
                    initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox2[i, p].Background = Brushes.Gray;
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
                    if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }
    }
}
