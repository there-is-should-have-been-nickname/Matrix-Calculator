using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для MultiplicationWindow.xaml
    /// </summary>
    /// 

    public partial class MultiplicationWindow : Window
    {

        private int rows1 { get; set; }
        private int columns1 { get; set; }

        private int rows2 { get; set; }
        private int columns2 { get; set; }

        private Grid innerGrid;

        private Matrix initialMatrix1;
        private TextBox[,] initialMatrixTextBox1;

        private Matrix initialMatrix2;
        private TextBox[,] initialMatrixTextBox2;

        private Matrix finalMatrix;
        private TextBox[,] finalMatrixTextBox;

        private Button buttonCalculate;

        System.Windows.Threading.DispatcherTimer subTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MultiplicationWindow()
        {
            InitializeComponent();
        }

        private void multiplicationFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (canParseRowsAndColumns())
            {
                parseRowsAndColumns();

                if (isSuitableSizes())
                {
                    creationAndInsertionInnerGrid();
                    creationAndInsertionInitialMatrixesTextBox();
                    creationAndInsertionOperators();
                    creationAndInsertionButtonCalculate();
                }
                else
                {
                    MessageBox.Show("Количество столбцов первой матрицы не равно количеству строк второй матрицы. Так умножать матрицы нельзя. Пожалуйста, сделайте их равными и попробуйте еще раз");
                }
                
                
            }
            else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            clearInitialMatrixTextBox1();

            if (canParseInitialMatrixesTextBox())
            {
                creationAndInsertionOperators();

                int[,] initialMatrixMas1 = new int[rows1, columns1];
                int[,] initialMatrixMas2 = new int[rows2, columns2];

                for (int i = 0; i < rows1; ++i)
                {
                    for (int p = 0; p < columns1; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox1[i, p].Text);
                        initialMatrixMas1[i, p] = elem;
                    }
                }
                for (int i = 0; i < rows2; ++i)
                {
                    for (int p = 0; p < columns2; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox2[i, p].Text);
                        initialMatrixMas2[i, p] = elem;
                    }
                }


                initialMatrix1 = new Matrix(rows1, columns1, initialMatrixMas1);
                initialMatrix2 = new Matrix(rows2, columns2, initialMatrixMas2);
                finalMatrix = initialMatrix1.multiplication(initialMatrix2);
                
                

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
            bool isParsedRows1 = int.TryParse(multiplicationFormRows1.Text, out int value);
            bool isParsedColumns1 = int.TryParse(multiplicationFormColumns1.Text, out int value2);

            bool isParsedRows2 = int.TryParse(multiplicationFormRows2.Text, out int value3);
            bool isParsedColumns2 = int.TryParse(multiplicationFormColumns2.Text, out int value4);

            if (isParsedRows1 && isParsedColumns1 && isParsedRows2 && isParsedColumns2)
            {
                return true;
            }
            return false;
        }

        private bool isSuitableSizes()
        {
            if (columns1 == rows2)
            {
                return true;
            }
            return false;
        }


        private void parseRowsAndColumns()
        {
            rows1 = Convert.ToInt32(multiplicationFormRows1.Text);
            columns1 = Convert.ToInt32(multiplicationFormColumns1.Text);

            rows2 = Convert.ToInt32(multiplicationFormRows2.Text);
            columns2 = Convert.ToInt32(multiplicationFormColumns2.Text);
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
            multiplicationFormGrid.Children.Add(innerGrid);
        }

        private void clearInitialMatrixTextBox1()
        {
            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns1; ++p)
                {
                    initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox1[i, p].Tag = 0;
                }
            }
        }

        private void clearInitialMatrixTextBox2()
        {
            for (int i = 0; i < rows2; ++i)
            {
                for (int p = 0; p < columns2; ++p)
                {
                    initialMatrixTextBox2[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox2[i, p].Tag = 0;
                }
            }
        }

        private void creationAndInsertionInitialMatrixesTextBox()
        {
            initialMatrixTextBox1 = new TextBox[rows1, columns1];
            initialMatrixTextBox2 = new TextBox[rows2, columns2];

            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns1; ++p)
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

            for (int i = 0; i < rows2; ++i)
            {
                for (int p = 0; p < columns2; ++p)
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
                    elemTextBox.Margin = new Thickness(20 + p * 50 + columns1 * 50 + 100, 20 + i * 50, 0, 0);
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
            operatorLabel.Content = "X";
            operatorLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorLabel.VerticalContentAlignment = VerticalAlignment.Center;

            operatorLabel.VerticalAlignment = VerticalAlignment.Top;
            operatorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            operatorLabel.Margin = new Thickness(columns1 * 50 + 50, rows1 * 50 / 2, 0, 0);
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
            operatorEqual.Margin = new Thickness((20 + columns2 * 50) / 2, rows2 * 50 + 50, 0, 0);
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
            multiplicationFormGrid.Children.Add(buttonCalculate);
        }

        private void creationAndInsertionFinalMatrix()
        {
            finalMatrixTextBox = new TextBox[rows1, columns2];

            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns2; ++p)
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

                    elemTextBox.Tag = 0;

                    elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                    elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                    elemTextBox.Margin = new Thickness(20 + p * 50 + columns1 * 50, 20 + i * 50 + rows2 * 50, 0, 0);
                    finalMatrixTextBox[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
        }

        private bool canParseInitialMatrixesTextBox()
        {
            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns1; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox1[i, p].Text, out int value);

                    if (!isParsed)
                    {
                        return false;
                    }
                }

            }

            for (int i = 0; i < rows2; ++i)
            {
                for (int p = 0; p < columns2; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox2[i, p].Text, out int value);

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
            timer.Interval = new TimeSpan(0, 0, 0, 0, columns1 * 1000);
            //timer.Start();

            subTimer.Tick += subTimerTick;
            subTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            subTimer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {

            if (isFinalMatrixCalculated())
            {
                //initialMatrixTextBox1[rows1 - 1, columns1 - 1].Background = Brushes.Gray;
                //initialMatrixTextBox2[rows2 - 1, columns2 - 1].Background = Brushes.Gray;
                finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
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
            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns2; ++p)
                {
                    if (Convert.ToInt32(finalMatrixTextBox[i, p].Tag) == 0)
                    {
                        //initialMatrixTextBox1[i, p].Background = Brushes.Green;
                        //initialMatrixTextBox2[i, p].Background = Brushes.Coral;
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                        finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                        finalMatrixTextBox[i, p].Tag = 1;
                        return;
                    }
                    //initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                    //initialMatrixTextBox2[i, p].Background = Brushes.Gray;
                    finalMatrixTextBox[i, p].Background = Brushes.Gray;

                }

            }
        }

        private bool isFinalMatrixCalculated()
        {
            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns2; ++p)
                {
                    if (Convert.ToInt32(finalMatrixTextBox[i, p].Tag) == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        private bool isAllMatrixCalculated()
        {
            for (int i = 0; i < rows1; ++i)
            {
                for (int p = 0; p < columns1; ++p)
                {
                    if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        private void subTimerTick(object sender, EventArgs e)
        {

            if (isAllMatrixCalculated())
            {
                initialMatrixTextBox1[rows1 - 1, columns1 - 1].Background = Brushes.Gray;
                initialMatrixTextBox2[rows2 - 1, columns2 - 1].Background = Brushes.Gray;
                
                if (columns2 == 1 && rows1 == 1)
                {
                    finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                } else if (columns2 == 1 && rows1 != 1)
                {
                    finalMatrixTextBox[rows1 - 2, columns2 - 1].Background = Brushes.Gray;
                } else /*(columns2 != 1 && rows1 == 1)*/
                {
                    finalMatrixTextBox[rows1 - 1, columns2 - 2].Background = Brushes.Gray;
                }
                
                finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                finalMatrixTextBox[rows1 - 1, columns2 - 1].Text = finalMatrix.nums[rows1 - 1, columns2 - 1].ToString();
                


                subTimer.Stop();
                subTimer.Tick -= subTimerTick;
            }
            else
            {
                //clearFinalMatrixTextBox();
                goToNextTextBox();
            }

        }
        //TODO: переменная глобально некрасиво
        bool isNextLine = false;

        //TODO: разбить на методы
        private void goToNextTextBox()
        {

            for (int i = 0; i < rows1; ++i)
            {
                if (isNextLine)
                {
                    clearInitialMatrixTextBox2();
                    isNextLine = false;
                }
                
                for (int p = 0; p < columns2; ++p)
                {
                    

                    for (int k = 0; k < columns1; ++k)
                    {
                        if (Convert.ToInt32(initialMatrixTextBox1[i, k].Tag) == 0 && Convert.ToInt32(initialMatrixTextBox2[k, p].Tag) == 0)
                        {
                            
    
                            initialMatrixTextBox1[i, k].Background = Brushes.Green;
                            initialMatrixTextBox2[k, p].Background = Brushes.Coral;
                            
                            
                            //finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                            //finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();

                            if (p == columns2 - 1)
                            {
                                initialMatrixTextBox1[i, k].Tag = 1;
                                if (k == columns1 - 1)
                                {
                                    isNextLine = true;
                                }
                            }
                            
                            
                            initialMatrixTextBox2[k, p].Tag = 1;
                            return;
                        }
                        initialMatrixTextBox1[i, k].Background = Brushes.Gray;
                        initialMatrixTextBox2[k, p].Background = Brushes.Gray;
                        //finalMatrixTextBox[i, p].Background = Brushes.Gray;
                    }

                    if (i == 0 && p == 0)
                    {
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                    } else if (i != 0 && p == 0) {
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                        finalMatrixTextBox[i - 1, columns2 - 1].Background = Brushes.Gray;
                    } else {
                        finalMatrixTextBox[i, p].Background = Brushes.Aqua;
                        finalMatrixTextBox[i, p - 1].Background = Brushes.Gray;
                    }
                    finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                    if (i == rows1 - 1 && p == columns2 - 1)
                    {
                        multiplicationFormButtonCreate.Content = "Ha";
                    }

                }

            }
        }

    }
}
