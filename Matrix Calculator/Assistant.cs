using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Matrix_Calculator
{
    public class Assistant
    {
        private int formNumber { get; set; }

        /// <summary>
        /// Parameters for window
        /// </summary>

        public Grid innerGrid;

        public TextBox[,] initialMatrixTextBox;
        public TextBox[,] initialMatrixTextBox1;
        public TextBox[,] initialMatrixTextBox2;

        public TextBox[,] finalMatrixTextBox;

        public TextBox numberTextBox;
        public TextBox determinantTextBox;

        public System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        ComboBox initialMatrixColor;
        ComboBox initialMatrix1Color;
        ComboBox initialMatrix2Color;
        ComboBox numberColor;
        ComboBox finalMatrixColor;
        Button buttonCreate;
        Button buttonCalculate;
        Button buttonBack;


        /// <summary>
        /// Parameters for calculation
        /// </summary>
        public int rows { get; set; }
        public int columns { get; set; }

        public int rows1 { get; set; }
        public int columns1 { get; set; }
        public int rows2 { get; set; }
        public int columns2 { get; set; }
        public int number { get; set; }
        public int determinant { get; set; }

        public Matrix initialMatrix;
        public Matrix initialMatrix1;
        public Matrix initialMatrix2;

        public Matrix finalMatrix;

        public Assistant()
        {

        }

        public Assistant(int formNumber, Button buttonCreate, Button buttonCalculate, 
            Button buttonBack, ComboBox initialMatrixColor, ComboBox numberColor, ComboBox finalMatrixColor)
        {
            this.formNumber = formNumber;
            this.buttonCreate = buttonCreate;
            this.buttonCalculate = buttonCalculate;
            this.buttonBack = buttonBack;
            if (formNumber == 1)
            {
                this.initialMatrixColor = initialMatrixColor;
                this.numberColor = numberColor;
            } else if (formNumber == 2 || formNumber == 3)
            {
                initialMatrix1Color = initialMatrixColor;
                initialMatrix2Color = numberColor;
            }
            
            this.finalMatrixColor = finalMatrixColor;
        }
        public Assistant(int formNumber, Button buttonCreate, Button buttonCalculate,
            Button buttonBack, ComboBox initialMatrixColor)
        {
            this.formNumber = formNumber;
            this.buttonCreate = buttonCreate;
            this.buttonCalculate = buttonCalculate;
            this.buttonBack = buttonBack;
            this.initialMatrixColor = initialMatrixColor;
            
        }


        public void enableColorsAndSpeed(ComboBox initialMatrixColor, 
            ComboBox numberColor, ComboBox finalMatrixColor, Slider speedLight, 
            Button buttonCalculate)
        {
            initialMatrixColor.IsEnabled = true;
            numberColor.IsEnabled = true;
            finalMatrixColor.IsEnabled = true;
            speedLight.IsEnabled = true;
            buttonCalculate.IsEnabled = true;
        }

        public void enableColorsAndSpeed(ComboBox initialMatrixColor, Slider speedLight,
            Button buttonCalculate)
        {
            initialMatrixColor.IsEnabled = true;
            speedLight.IsEnabled = true;
            buttonCalculate.IsEnabled = true;
        }

        public bool canParseRowsAndColumns(ComboBox rows, ComboBox columns)
        {
            bool isParsedRows = int.TryParse(rows.Text, out int value);
            bool isParsedColumns = int.TryParse(columns.Text, out int value2);

            if (isParsedRows && isParsedColumns)
            {
                return true;
            }
            return false;
        }

        public bool canParseRowsAndColumns(ComboBox rows1, ComboBox columns1, ComboBox rows2,
            ComboBox columns2)
        {
            bool isParsedRows1 = int.TryParse(rows1.Text, out int value1);
            bool isParsedRows2 = int.TryParse(rows2.Text, out int value2);
            bool isParsedColumns1 = int.TryParse(columns1.Text, out int value3);
            bool isParsedColumns2 = int.TryParse(columns2.Text, out int value4);

            if (isParsedRows1 && isParsedColumns1 && isParsedRows2 && isParsedColumns2)
            {
                return true;
            }
            return false;
        }

        public bool canParseOperator(ComboBox operatorL)
        {
            if (string.IsNullOrWhiteSpace(operatorL.Text))
            {
                return false;
            }
            return true;
        }

        public bool canParseOrder(ComboBox order)
        {
            bool isParsedOrder = int.TryParse(order.Text, out int value);

            if (isParsedOrder)
            {
                return true;
            }
            return false;
        }

        public void parseRowsAndColumns(ComboBox rows, ComboBox columns)
        {
            this.rows = Convert.ToInt32(rows.Text);
            this.columns = Convert.ToInt32(columns.Text);
        }

        public void parseRowsAndColumns(ComboBox rows1, ComboBox columns1, ComboBox rows2,
            ComboBox columns2)
        {
            this.rows1 = Convert.ToInt32(rows1.Text);
            this.columns1 = Convert.ToInt32(columns1.Text);
            this.rows2 = Convert.ToInt32(rows2.Text);
            this.columns2 = Convert.ToInt32(columns2.Text);
        }

        public void parseOrder(ComboBox order)
        {
            rows = Convert.ToInt32(order.Text);
            columns = rows;
        }

        public bool isSuitableSizes()
        {
            if (columns1 == rows2)
            {
                return true;
            }
            return false;
        }

        public void setHeightWindow(Window window)
        {
            if (formNumber == 1 || formNumber == 4)
            {
                window.Height = 90 + rows * 40 + 20 * 2 + (rows - 1) * 10 + 90;
            } else if (formNumber == 2)
            {
                window.Height = 150 + rows * 40 + 20 * 2 + (rows - 1) * 10 + 90;
            } else if (formNumber == 3)
            {
                int biggerRow = (rows1 >= rows2) ? rows1 : rows2;
                window.Height = 130 + biggerRow * 40 + 20 * 2 + (biggerRow - 1) * 10 + 90;
            }
        }

        public void creationAndInsertionInnerGrid(Grid grid, Window window)
        {
            if (grid.Children.Contains(innerGrid))
            {
                grid.Children.Remove(innerGrid);
            }

            innerGrid = new Grid();
            innerGrid.Name = "innerGrid";
            innerGrid.HorizontalAlignment = HorizontalAlignment.Left;
            innerGrid.VerticalAlignment = VerticalAlignment.Top;
            innerGrid.Width = window.Width - 50 - 50;

            if (formNumber == 1 || formNumber == 4)
            {
                innerGrid.Margin = new Thickness(50, 90, 0, 0);
                innerGrid.Height = window.Height - 90 - 90;
            } else if (formNumber == 2)
            {
                innerGrid.Margin = new Thickness(50, 150, 0, 0);
                innerGrid.Height = window.Height - 150 - 90;
            } else if (formNumber == 3)
            {
                innerGrid.Margin = new Thickness(50, 130, 0, 0);
                innerGrid.Height = window.Height - 130 - 90;
            }

            //TODO: delete background
            innerGrid.Background = Brushes.Gray;
            grid.Children.Add(innerGrid);
        }

        public void clearInitialMatrixTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    initialMatrixTextBox[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox[i, p].Tag = 0;
                }
            }
        }

        public void clearInitialMatrixTextBox1()
        {
            if (formNumber == 2)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                        initialMatrixTextBox1[i, p].Tag = 0;
                    }
                }
            } else if (formNumber == 3)
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
        }

        public void clearInitialMatrixTextBox2()
        {
            if (formNumber == 2)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        initialMatrixTextBox2[i, p].Background = Brushes.Gray;
                        initialMatrixTextBox2[i, p].Tag = 0;
                    }
                }
            } else if (formNumber == 3)
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
            
        }

        public void creationAndInsertionInitialMatrixTextBox()
        {
            initialMatrixTextBox = new TextBox[rows, columns];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    var elemTextBox = new TextBox();
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

        public void creationAndInsertionDeterminantTextBox()
        {
            determinantTextBox = new TextBox();
            determinantTextBox.FontFamily = new FontFamily("Consolas");
            determinantTextBox.FontSize = 18;
            determinantTextBox.Width = 40;
            determinantTextBox.Height = 40;
            determinantTextBox.Text = "";
            determinantTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            determinantTextBox.VerticalContentAlignment = VerticalAlignment.Center;

            determinantTextBox.Background = Brushes.Gray;
            determinantTextBox.IsReadOnly = true;

            determinantTextBox.VerticalAlignment = VerticalAlignment.Top;
            determinantTextBox.HorizontalAlignment = HorizontalAlignment.Left;
            determinantTextBox.Margin = new Thickness(columns * 50 + 100, rows * 50 / 2 - 5, 0, 0);
            innerGrid.Children.Add(determinantTextBox);
        }

        public void creationAndInsertionOperatorLabel()
        {
            var operatorLabel = new Label();
            operatorLabel.FontFamily = new FontFamily("Consolas");
            operatorLabel.FontSize = 14;

            operatorLabel.Width = 40;
            operatorLabel.Height = 40;
            operatorLabel.Content = "*";
            operatorLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorLabel.VerticalContentAlignment = VerticalAlignment.Center;

            operatorLabel.VerticalAlignment = VerticalAlignment.Top;
            operatorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            if (formNumber == 1)
            {
                operatorLabel.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 10, innerGrid.Height / 2 - 20, 0, 0);
            } else if (formNumber == 3)
            {
                operatorLabel.Margin = new Thickness(20 + columns1 * 40 + (columns1 - 1) * 10 + 10, innerGrid.Height / 2 - 20, 0, 0);
            }
            
            innerGrid.Children.Add(operatorLabel);
        }

        public void creationAndInsertionOperatorLabel(ComboBox operatorL)
        {
            var operatorLabel = new Label();
            operatorLabel.FontFamily = new FontFamily("Consolas");
            operatorLabel.FontSize = 14;

            operatorLabel.Width = 40;
            operatorLabel.Height = 40;
            operatorLabel.Content = operatorL.Text;
            operatorLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorLabel.VerticalContentAlignment = VerticalAlignment.Center;

            operatorLabel.VerticalAlignment = VerticalAlignment.Top;
            operatorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            operatorLabel.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 10, innerGrid.Height / 2 - 20, 0, 0);
            innerGrid.Children.Add(operatorLabel);
        }

        public void creationAndInsertionNumberTextBox()
        {
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
            numberTextBox.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 60, innerGrid.Height / 2 - 20, 0, 0);
            innerGrid.Children.Add(numberTextBox);
        }

        public void creationAndInsertionInitialMatrixTextBox1()
        {
            if (formNumber == 2)
            {
                initialMatrixTextBox1 = new TextBox[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        var elemTextBox = new TextBox();
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
            } else if (formNumber == 3)
            {
                initialMatrixTextBox1 = new TextBox[rows1, columns1];

                for (int i = 0; i < rows1; ++i)
                {
                    for (int p = 0; p < columns1; ++p)
                    {
                        var elemTextBox = new TextBox();
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
            }

            clearInitialMatrixTextBox1();

        }

        public void creationAndInsertionInitialMatrixTextBox2()
        {
            if (formNumber == 2)
            {
                initialMatrixTextBox2 = new TextBox[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        var elemTextBox = new TextBox();
                        elemTextBox.FontFamily = new FontFamily("Consolas");
                        elemTextBox.FontSize = 18;
                        elemTextBox.Width = 40;
                        elemTextBox.Height = 40;
                        elemTextBox.Text = "";
                        elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                        elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;

                        elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                        elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                        elemTextBox.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 60 + p * 50, 20 + i * 50, 0, 0);
                        initialMatrixTextBox2[i, p] = elemTextBox;

                        innerGrid.Children.Add(elemTextBox);
                    }
                }
            } else if (formNumber == 3)
            {
                initialMatrixTextBox2 = new TextBox[rows2, columns2];

                for (int i = 0; i < rows2; ++i)
                {
                    for (int p = 0; p < columns2; ++p)
                    {
                        var elemTextBox = new TextBox();
                        elemTextBox.FontFamily = new FontFamily("Consolas");
                        elemTextBox.FontSize = 18;
                        elemTextBox.Width = 40;
                        elemTextBox.Height = 40;
                        elemTextBox.Text = "";
                        elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                        elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;

                        elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                        elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                        elemTextBox.Margin = new Thickness(20 + columns1 * 40 + (columns1 - 1) * 10 + 60 + p * 50, 20 + i * 50, 0, 0);
                        initialMatrixTextBox2[i, p] = elemTextBox;

                        innerGrid.Children.Add(elemTextBox);
                    }
                }
            }
            
            clearInitialMatrixTextBox2();
        }
        public void creationAndInsertionEqualLabel()
        {
            var operatorEqual = new Label();
            operatorEqual.FontFamily = new FontFamily("Consolas");
            operatorEqual.FontSize = 14;

            operatorEqual.Width = 40;
            operatorEqual.Height = 40;
            operatorEqual.Content = "=";
            operatorEqual.HorizontalContentAlignment = HorizontalAlignment.Center;
            operatorEqual.VerticalContentAlignment = VerticalAlignment.Center;


            operatorEqual.VerticalAlignment = VerticalAlignment.Top;
            operatorEqual.HorizontalAlignment = HorizontalAlignment.Left;
            if (formNumber == 1)
            {
                operatorEqual.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 110, innerGrid.Height / 2 - 20, 0, 0);
            } else if (formNumber == 2)
            {
                operatorEqual.Margin = new Thickness(20 + columns * 40 * 2 + (columns - 1) * 10 * 2 + 70, innerGrid.Height / 2 - 20, 0, 0);
            } else if (formNumber == 3)
            {
                operatorEqual.Margin = new Thickness(20 + columns1 * 40 + (columns1 - 1) * 10 + 70 + columns2 * 40 + (columns2 - 1) * 10, innerGrid.Height / 2 - 20, 0, 0);
            }
            innerGrid.Children.Add(operatorEqual);
        }

        public bool canParseInitialMatrixTextBox()
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox[i, p].Text, out int value);

                    if (!isParsed)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public bool canParseInitialMatrixesTextBox()
        {
            if (formNumber == 2)
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

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        bool isParsed = int.TryParse(initialMatrixTextBox2[i, p].Text, out int value);

                        if (!isParsed)
                        {
                            return false;
                        }
                    }

                }

                return true;
            } else if (formNumber == 3)
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
            return true;
        }

        public bool canParseNumberTextBox()
        {
            bool isParsed = int.TryParse(numberTextBox.Text, out int value);

            if (!isParsed)
            {
                return false;
            }
            return true;
        }

        public bool canDefineColors(ComboBox initialMatrixColor,
            ComboBox numberColor, ComboBox finalMatrixColor)
        {
            if (!string.IsNullOrWhiteSpace(initialMatrixColor.Text)
                && !string.IsNullOrWhiteSpace(numberColor.Text)
                && !string.IsNullOrWhiteSpace(finalMatrixColor.Text))
            {
                return true;
            }
            return false;
        }

        public bool canDefineColors(ComboBox initialMatrixColor)
        {
            if (!string.IsNullOrWhiteSpace(initialMatrixColor.Text))
            {
                return true;
            }
            return false;
        }

        public Matrix getInitialMatrix()
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

            return new Matrix(rows, columns, initialMatrixMas);
        }
        public Matrix getInitialMatrix1()
        {
            if (formNumber == 2)
            {
                int[,] initialMatrixMas = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox1[i, p].Text);
                        initialMatrixMas[i, p] = elem;
                    }
                }

                return new Matrix(rows, columns, initialMatrixMas);
            } else if (formNumber == 3)
            {
                int[,] initialMatrixMas = new int[rows1, columns1];

                for (int i = 0; i < rows1; ++i)
                {
                    for (int p = 0; p < columns1; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox1[i, p].Text);
                        initialMatrixMas[i, p] = elem;
                    }
                }

                return new Matrix(rows1, columns1, initialMatrixMas);
            }
            return null;
        }

        public Matrix getInitialMatrix2()
        {
            if (formNumber == 2)
            {
                int[,] initialMatrixMas = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox2[i, p].Text);
                        initialMatrixMas[i, p] = elem;
                    }
                }

                return new Matrix(rows, columns, initialMatrixMas);
            } else if (formNumber == 3)
            {
                int[,] initialMatrixMas = new int[rows2, columns2];

                for (int i = 0; i < rows2; ++i)
                {
                    for (int p = 0; p < columns2; ++p)
                    {
                        int elem = Convert.ToInt32(initialMatrixTextBox2[i, p].Text);
                        initialMatrixMas[i, p] = elem;
                    }
                }

                return new Matrix(rows2, columns2, initialMatrixMas);
            }
            return null;
        }

        public void creationAndInsertionFinalMatrix()
        {
            if (formNumber == 1 || formNumber == 2)
            {
                finalMatrixTextBox = new TextBox[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        var elemTextBox = new TextBox();
                        elemTextBox.FontFamily = new FontFamily("Consolas");
                        elemTextBox.FontSize = 18;
                        elemTextBox.Width = 40;
                        elemTextBox.Height = 40;
                        elemTextBox.Text = "";
                        elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                        elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;
                        elemTextBox.Background = Brushes.Gray;
                        elemTextBox.IsReadOnly = true;

                        elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                        elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                        if (formNumber == 1)
                        {
                            elemTextBox.Margin = new Thickness(20 + columns * 40 + (columns - 1) * 10 + 150 + 10 + p * 50, 20 + i * 50, 0, 0);
                        }
                        else if (formNumber == 2)
                        {
                            elemTextBox.Margin = new Thickness(20 + columns * 40 * 2 + (columns - 1) * 10 * 2 + 70 + 50 + p * 50, 20 + i * 50, 0, 0);
                        }

                        finalMatrixTextBox[i, p] = elemTextBox;

                        innerGrid.Children.Add(elemTextBox);
                    }
                }
            } else if (formNumber == 3)
            {
                finalMatrixTextBox = new TextBox[rows1, columns2];

                for (int i = 0; i < rows1; ++i)
                {
                    for (int p = 0; p < columns2; ++p)
                    {
                        var elemTextBox = new TextBox();
                        elemTextBox.FontFamily = new FontFamily("Consolas");
                        elemTextBox.FontSize = 18;
                        elemTextBox.Width = 40;
                        elemTextBox.Height = 40;
                        elemTextBox.Text = "";
                        elemTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                        elemTextBox.VerticalContentAlignment = VerticalAlignment.Center;
                        elemTextBox.Background = Brushes.Gray;
                        elemTextBox.IsReadOnly = true;

                        elemTextBox.VerticalAlignment = VerticalAlignment.Top;
                        elemTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                        elemTextBox.Margin = new Thickness(20 + columns1 * 40 + (columns1 - 1) * 10 + 70 + columns2 * 40 + (columns2 - 1) * 10 + 50 + p * 50, 20 + i * 50, 0, 0);
                        

                        finalMatrixTextBox[i, p] = elemTextBox;

                        innerGrid.Children.Add(elemTextBox);
                    }
                }
            }
            
        }

        public bool isAllMatrixCalculated()
        {
            if (formNumber == 1 || formNumber == 2)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        if (formNumber == 1)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) == 0)
                            {
                                return false;
                            }
                        }
                        else if (formNumber == 2)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                            {
                                return false;
                            }
                        }

                    }

                }

                return true;
            } else if (formNumber == 3)
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
            } else if (formNumber == 4)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) < 2)
                        {
                            return false;
                        }
                    }

                }
                return true;
            }
            return true;
        }

        public SolidColorBrush getColor(ComboBox comboBox)
        {
            switch (comboBox.Text)
            {
                case "Зеленый":
                    return Brushes.Green;
                case "Красный":
                    return Brushes.Red;
                case "Синий":
                    return Brushes.Blue;
                case "Белый":
                    return Brushes.White;
                case "Оранжевый":
                    return Brushes.Orange;
                case "Желтый":
                    return Brushes.Yellow;
                case "Розовый":
                    return Brushes.Pink;
                case "Фиолетовый":
                    return Brushes.Purple;
                case "Серебряный":
                    return Brushes.Silver;
                case "Золотой":
                    return Brushes.Gold;
                default:
                    return Brushes.Gray;
            }
        }

        private bool isNextLine = false;

        public void fillNextTextBox()
        {
            if (formNumber == 1 || formNumber == 2)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        if (formNumber == 1)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) == 0)
                            {
                                initialMatrixTextBox[i, p].Background = getColor(initialMatrixColor);
                                numberTextBox.Background = getColor(numberColor);
                                finalMatrixTextBox[i, p].Background = getColor(finalMatrixColor);

                                finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                                initialMatrixTextBox[i, p].Tag = 1;
                                return;
                            }
                            initialMatrixTextBox[i, p].Background = Brushes.Gray;
                            finalMatrixTextBox[i, p].Background = Brushes.Gray;
                        }
                        else if (formNumber == 2)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                            {
                                initialMatrixTextBox1[i, p].Background = getColor(initialMatrix1Color);
                                initialMatrixTextBox2[i, p].Background = getColor(initialMatrix2Color);
                                finalMatrixTextBox[i, p].Background = getColor(finalMatrixColor);
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
            } else if (formNumber == 3)
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


                                initialMatrixTextBox1[i, k].Background = getColor(initialMatrix1Color);
                                initialMatrixTextBox2[k, p].Background = getColor(initialMatrix2Color);

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
                        }

                        finalMatrixTextBox[i, p].Background = getColor(finalMatrixColor);
                        
                        if (i != 0 && p == 0)
                        {
                            finalMatrixTextBox[i - 1, columns2 - 1].Background = Brushes.Gray;
                        } else if (i == rows1 - 1 && p == columns2 - 1)
                        {
                            finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                        }
                        else if (p != 0)
                        {
                            finalMatrixTextBox[i, p - 1].Background = Brushes.Gray;
                        }
                        finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                    }

                }
            }
            
        }

        public void fillNextTextBox(int order)
        {
            var color = getColor(initialMatrixColor);
            if (order == 1)
            {
                initialMatrixTextBox[0, 0].Background = color;
                initialMatrixTextBox[0, 0].Tag = 2;
                return;
            }
            else if (order == 2)
            {
                for (int i = 0; i < rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, i].Background = color;
                        initialMatrixTextBox[i, i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, i].Background = Brushes.Gray;

                }

                for (int i = 0; i < rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, rows - 1 - i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, rows - 1 - i].Background = color;
                        initialMatrixTextBox[i, rows - 1 - i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Gray;

                }
            }
            else if (order == 3)
            {
                for (int i = 0; i < rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, i].Background = color;
                        initialMatrixTextBox[i, i].Tag = 1;
                        return;
                    }
                    initialMatrixTextBox[i, i].Background = Brushes.Gray;

                }

                if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 0)
                {
                    initialMatrixTextBox[1, 0].Background = color;
                    initialMatrixTextBox[1, 0].Tag = 1;
                    return;
                }
                initialMatrixTextBox[1, 0].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 0)
                {
                    initialMatrixTextBox[2, 1].Background = color;
                    initialMatrixTextBox[2, 1].Tag = 1;
                    return;
                }
                initialMatrixTextBox[2, 1].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[0, 2].Tag) == 0)
                {
                    initialMatrixTextBox[0, 2].Background = color;
                    initialMatrixTextBox[0, 2].Tag = 1;
                    return;
                }
                initialMatrixTextBox[0, 2].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 0)
                {
                    initialMatrixTextBox[0, 1].Background = color;
                    initialMatrixTextBox[0, 1].Tag = 1;
                    return;
                }
                initialMatrixTextBox[0, 1].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 0)
                {
                    initialMatrixTextBox[1, 2].Background = color;
                    initialMatrixTextBox[1, 2].Tag = 1;
                    return;
                }
                initialMatrixTextBox[1, 2].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[2, 0].Tag) == 0)
                {
                    initialMatrixTextBox[2, 0].Background = color;
                    initialMatrixTextBox[2, 0].Tag = 1;
                    return;
                }
                initialMatrixTextBox[2, 0].Background = Brushes.Gray;

                for (int i = 0; i < rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, rows - 1 - i].Tag) == 1)
                    {
                        initialMatrixTextBox[i, rows - 1 - i].Background = color;
                        initialMatrixTextBox[i, rows - 1 - i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, rows - 1 - i].Background = Brushes.Gray;

                }

                if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 1)
                {
                    initialMatrixTextBox[1, 2].Background = color;
                    initialMatrixTextBox[1, 2].Tag = 2;
                    return;
                }
                initialMatrixTextBox[1, 2].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 1)
                {
                    initialMatrixTextBox[2, 1].Background = color;
                    initialMatrixTextBox[2, 1].Tag = 2;
                    return;
                }
                initialMatrixTextBox[2, 1].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[0, 0].Tag) == 1)
                {
                    initialMatrixTextBox[0, 0].Background = color;
                    initialMatrixTextBox[0, 0].Tag = 2;
                    return;
                }
                initialMatrixTextBox[0, 0].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 1)
                {
                    initialMatrixTextBox[1, 0].Background = color;
                    initialMatrixTextBox[1, 0].Tag = 2;
                    return;
                }
                initialMatrixTextBox[1, 0].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 1)
                {
                    initialMatrixTextBox[0, 1].Background = color;
                    initialMatrixTextBox[0, 1].Tag = 2;
                    return;
                }
                initialMatrixTextBox[0, 1].Background = Brushes.Gray;

                if (Convert.ToInt32(initialMatrixTextBox[2, 2].Tag) == 1)
                {
                    initialMatrixTextBox[2, 2].Background = color;
                    initialMatrixTextBox[2, 2].Tag = 2;
                    return;
                }
            }

        }

        public void manageButtons(bool state)
        {
            buttonCreate.IsEnabled = state;
            buttonCalculate.IsEnabled = state;
            buttonBack.IsEnabled = state;
        }
        public void timerTick(object sender, EventArgs e)
        {
            if (isAllMatrixCalculated())
            {
                if (formNumber == 1)
                {
                    initialMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                    finalMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                    numberTextBox.Background = Brushes.Gray;
                } else if (formNumber == 2)
                {
                    initialMatrixTextBox1[rows - 1, columns - 1].Background = Brushes.Gray;
                    initialMatrixTextBox2[rows - 1, columns - 1].Background = Brushes.Gray;
                    finalMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                } else if (formNumber == 3)
                {
                    initialMatrixTextBox1[rows1 - 1, columns1 - 1].Background = Brushes.Gray;
                    initialMatrixTextBox2[rows2 - 1, columns2 - 1].Background = Brushes.Gray;

                    if (columns2 == 1 && rows1 == 1)
                    {
                        finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                    }
                    else if (columns2 == 1 && rows1 != 1)
                    {
                        finalMatrixTextBox[rows1 - 2, columns2 - 1].Background = Brushes.Gray;
                    }
                    else
                    {
                        finalMatrixTextBox[rows1 - 1, columns2 - 2].Background = Brushes.Gray;
                    }

                    finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                    finalMatrixTextBox[rows1 - 1, columns2 - 1].Text = finalMatrix.nums[rows1 - 1, columns2 - 1].ToString();

                } else if (formNumber == 4)
                {
                    initialMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                    determinantTextBox.Text = determinant.ToString();
                }

                timer.Tick -= timerTick;
                timer.Stop();
                manageButtons(true);
            }
            else
            {
                if (formNumber < 4)
                {
                    fillNextTextBox();
                } else
                {
                    fillNextTextBox(rows);
                }
                
            }

        }

        public void activateTimer(Button buttonCreate, Button buttonCalculate, Button buttonBack, Label speedLightLabel)
        {
            timer.Tick += timerTick;

            var interval = Convert.ToInt32(speedLightLabel.Content);
            timer.Interval = new TimeSpan(0, 0, interval);
            timer.Start();

            manageButtons(false);
        }
    }
}
