using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Matrix_Calculator
{
    public class Assistant
    {
        private int formNumber { get; set; }
        private bool isNextLine = false;

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

        public ComboBox initialMatrixColor;
        public ComboBox initialMatrix1Color;
        public ComboBox initialMatrix2Color;
        public ComboBox numberColor;
        public ComboBox finalMatrixColor;
        public Button buttonCreate;
        public Button buttonCalculate;
        public Button buttonBack;

        public System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

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

        private void setNumberAndButtons(int formNumber, Button buttonCreate, Button buttonCalculate,
            Button buttonBack)
        {
            this.formNumber = formNumber;
            this.buttonCreate = buttonCreate;
            this.buttonCalculate = buttonCalculate;
            this.buttonBack = buttonBack;
        }

        private void setInitialGraphicSettings(Grid grid, Window window, int marginTop)
        {
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Width = window.Width - 50 - 50;
            grid.Background = Brushes.Gray;
            grid.Margin = new Thickness(50, marginTop, 0, 0);
            grid.Height = window.Height - marginTop - 90;
        }

        private void setInitialGraphicSettings(Control element, int fontSize, int marginLeft, int marginTop)
        {
            element.FontFamily = new FontFamily("Consolas");
            element.FontSize = fontSize;
            element.Width = 40;
            element.Height = 40;
            element.HorizontalContentAlignment = HorizontalAlignment.Center;
            element.VerticalContentAlignment = VerticalAlignment.Center;
            element.VerticalAlignment = VerticalAlignment.Top;
            element.HorizontalAlignment = HorizontalAlignment.Left;
            element.Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }

        private void setExtraGraphicSettings(TextBox textbox, bool stateReadOnly = false)
        {
            textbox.Text = "";
            textbox.Background = Brushes.Gray;
            textbox.IsReadOnly = stateReadOnly;
        }

        private bool checkMatrix(TextBox[,] matrixTextBox, int indI, int indJ, int stopNum)
        {
            for (int i = 0; i < indI; ++i)
            {
                for (int p = 0; p < indJ; ++p)
                {
                    if (Convert.ToInt32(matrixTextBox[i, p].Tag) != stopNum)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void fillTextBoxDependingOnIndexes(int indI, int indJ)
        {
            finalMatrixTextBox[indI, indJ].Background = getColor(finalMatrixColor);

            if (indI != 0 && indJ == 0)
            {
                finalMatrixTextBox[indI - 1, columns2 - 1].Background = Brushes.Gray;
            }
            else if (indI == rows1 - 1 && indJ == columns2 - 1)
            {
                finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
            }
            else if (indJ != 0)
            {
                finalMatrixTextBox[indI, indJ - 1].Background = Brushes.Gray;
            }
        }

        private void fillTextBoxByColor(TextBox textBox1, TextBox textBox2, 
            TextBox textBox3, ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3)
        {
            textBox1.Background = getColor(comboBox1);
            textBox2.Background = getColor(comboBox2);
            textBox3.Background = getColor(comboBox3);
        }
        private void fillTextBoxByColor(TextBox textBox1, TextBox textBox2,
            ComboBox comboBox1, ComboBox comboBox2)
        {
            textBox1.Background = getColor(comboBox1);
            textBox2.Background = getColor(comboBox2);
        }

        private void fillTextBoxByGray(TextBox textBox1, TextBox textBox2,
            TextBox textBox3)
        {
            textBox1.Background = Brushes.Gray;
            textBox2.Background = Brushes.Gray;
            textBox3.Background = Brushes.Gray;
        }
        private void fillTextBoxByGray(TextBox textBox1, TextBox textBox2)
        {
            textBox1.Background = Brushes.Gray;
            textBox2.Background = Brushes.Gray;
        }

        private void fillTextBoxDependingOnIndexes()
        {
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
        }

        public Assistant()
        {
            
        }

        public Assistant(int formNumber, Button buttonCreate, Button buttonCalculate, 
            Button buttonBack, ComboBox initialMatrixColor, ComboBox numberColor, ComboBox finalMatrixColor)
        {
            setNumberAndButtons(formNumber, buttonCreate, buttonCalculate, buttonBack);

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
            setNumberAndButtons(formNumber, buttonCreate, buttonCalculate, buttonBack);
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
            return int.TryParse(rows.Text, out int value) && int.TryParse(columns.Text, out int value2);
        }

        public bool canParseRowsAndColumns(ComboBox rows1, ComboBox columns1, ComboBox rows2,
            ComboBox columns2)
        {
            return int.TryParse(rows1.Text, out int value1) 
                && int.TryParse(rows2.Text, out int value2) 
                && int.TryParse(columns1.Text, out int value3) 
                && int.TryParse(columns2.Text, out int value4);
        }

        public bool canParseOperator(ComboBox operatorL)
        {
            return !string.IsNullOrWhiteSpace(operatorL.Text);
        }

        public bool canParseOrder(ComboBox order)
        {
            return int.TryParse(order.Text, out int value);
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
            return columns1 == rows2;
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

            int marginTop = 0;

            if (formNumber == 1 || formNumber == 4)
            {
                marginTop = 90;
            }
            else if (formNumber == 2)
            {
                marginTop = 150;
            }
            else if (formNumber == 3)
            {
                marginTop = 130;
            }

            setInitialGraphicSettings(innerGrid, window, marginTop);
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
            int indexI = 0, indexJ = 0;
            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
            } else if (formNumber == 3)
            {
                indexI = rows1;
                indexJ = columns1;
            }

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    initialMatrixTextBox1[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox1[i, p].Tag = 0;
                }
            }
        }

        public void clearInitialMatrixTextBox2()
        {
            int indexI = 0, indexJ = 0;

            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
            } else if (formNumber == 3)
            {
                indexI = rows2;
                indexJ = columns2;
            }
            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    initialMatrixTextBox2[i, p].Background = Brushes.Gray;
                    initialMatrixTextBox2[i, p].Tag = 0;
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
                    setInitialGraphicSettings(elemTextBox, 18, 20 + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox();
        }

        public void creationAndInsertionDeterminantTextBox()
        {
            determinantTextBox = new TextBox();
            setInitialGraphicSettings(determinantTextBox, 18, columns * 50 + 100, rows * 50 / 2 - 5);
            setExtraGraphicSettings(determinantTextBox, true);
            innerGrid.Children.Add(determinantTextBox);
        }

        public void creationAndInsertionOperatorLabel()
        {
            var operatorLabel = new Label();

            int marginLeft = 0;
            if (formNumber == 1)
            {
                marginLeft = 20 + columns * 40 + (columns - 1) * 10 + 10;
            } else if (formNumber == 3)
            {
                marginLeft = 20 + columns1 * 40 + (columns1 - 1) * 10 + 10;
            }
            
            setInitialGraphicSettings(operatorLabel, 14, marginLeft, (int)innerGrid.Height / 2 - 20);
            operatorLabel.Content = "*";
            innerGrid.Children.Add(operatorLabel);
        }

        public void creationAndInsertionOperatorLabel(ComboBox operatorL)
        {
            var operatorLabel = new Label();
            setInitialGraphicSettings(operatorLabel, 14, 20 + columns * 40 + (columns - 1) * 10 + 10, (int)innerGrid.Height / 2 - 20);
            operatorLabel.Content = operatorL.Text;
            innerGrid.Children.Add(operatorLabel);
        }

        public void creationAndInsertionNumberTextBox()
        {
            numberTextBox = new TextBox();
            setInitialGraphicSettings(numberTextBox, 18, 20 + columns * 40 + (columns - 1) * 10 + 60, (int)innerGrid.Height / 2 - 20);
            setExtraGraphicSettings(numberTextBox);
            innerGrid.Children.Add(numberTextBox);
        }

        public void creationAndInsertionInitialMatrixTextBox1()
        {
            int indexI = 0, indexJ = 0;
            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
            } else if (formNumber == 3)
            {
                indexI = rows1;
                indexJ = columns1;
            }
            initialMatrixTextBox1 = new TextBox[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    setInitialGraphicSettings(elemTextBox, 18, 20 + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox1[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox1();
        }

        public void creationAndInsertionInitialMatrixTextBox2()
        {
            int indexI = 0, indexJ = 0, marginLeft = 0;

            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
                marginLeft = 20 + columns * 40 + (columns - 1) * 10 + 60;
            } else if (formNumber == 3)
            {
                indexI = rows2;
                indexJ = columns2;
                marginLeft = 20 + columns1 * 40 + (columns1 - 1) * 10 + 60;
            }

            initialMatrixTextBox2 = new TextBox[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    setInitialGraphicSettings(elemTextBox, 18, marginLeft + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox2[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            clearInitialMatrixTextBox2();
        }
        public void creationAndInsertionEqualLabel()
        {
            var operatorEqual = new Label();

            int marginLeft = 0;
            if (formNumber == 1)
            {
                marginLeft = 20 + columns * 40 + (columns - 1) * 10 + 110;
            } else if (formNumber == 2)
            {
                marginLeft = 20 + columns * 40 * 2 + (columns - 1) * 10 * 2 + 70;                
            } else if (formNumber == 3)
            {
                marginLeft = 20 + columns1 * 40 + (columns1 - 1) * 10 + 70 + columns2 * 40 + (columns2 - 1) * 10;
            }
            setInitialGraphicSettings(operatorEqual, 14, marginLeft, (int)innerGrid.Height / 2 - 20);
            operatorEqual.Content = "=";
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
                        bool isParsed1 = int.TryParse(initialMatrixTextBox1[i, p].Text, out int value1);
                        bool isParsed2 = int.TryParse(initialMatrixTextBox2[i, p].Text, out int value2);

                        if (!isParsed1 || !isParsed2)
                        {
                            return false;
                        }
                    }
                }
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
            }
            return true;
        }

        public bool canParseNumberTextBox()
        {
            return int.TryParse(numberTextBox.Text, out int value);
        }

        public bool canDefineColors(ComboBox initialMatrixColor,
            ComboBox numberColor, ComboBox finalMatrixColor)
        {
            return !string.IsNullOrWhiteSpace(initialMatrixColor.Text)
                && !string.IsNullOrWhiteSpace(numberColor.Text)
                && !string.IsNullOrWhiteSpace(finalMatrixColor.Text);
        }

        public bool canDefineColors(ComboBox initialMatrixColor)
        {
            return !string.IsNullOrWhiteSpace(initialMatrixColor.Text);
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
            int indexI = 0, indexJ = 0;
            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
            } else if (formNumber == 3)
            {
                indexI = rows1;
                indexJ = columns1;
            }
            int[,] initialMatrixMas = new int[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    int elem = Convert.ToInt32(initialMatrixTextBox1[i, p].Text);
                    initialMatrixMas[i, p] = elem;
                }
            }

            return new Matrix(indexI, indexJ, initialMatrixMas);
        }

        public Matrix getInitialMatrix2()
        {
            int indexI = 0, indexJ = 0;
            if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
            } else if (formNumber == 3)
            {
                indexI = rows2;
                indexJ = columns2;
            }

            int[,] initialMatrixMas = new int[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    int elem = Convert.ToInt32(initialMatrixTextBox2[i, p].Text);
                    initialMatrixMas[i, p] = elem;
                }
            }

            return new Matrix(indexI, indexJ, initialMatrixMas);
        }
        //TODO: fdsgf
        public void creationAndInsertionFinalMatrix()
        {
            int indexI = 0, indexJ = 0, marginLeft = 0;
            if (formNumber == 1)
            {
                indexI = rows;
                indexJ = columns;
                marginLeft = 20 + columns * 40 + (columns - 1) * 10 + 150 + 10;
            } else if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
                marginLeft = 20 + columns * 40 * 2 + (columns - 1) * 10 * 2 + 70 + 50;
            } else if (formNumber == 3)
            {
                indexI = rows1;
                indexJ = columns2;
                marginLeft = 20 + columns1 * 40 + (columns1 - 1) * 10 + 70 + columns2 * 40 + (columns2 - 1) * 10 + 50;
            }
            finalMatrixTextBox = new TextBox[indexI, indexJ];
            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    setInitialGraphicSettings(elemTextBox, 18, marginLeft + p * 50, 20 + i * 50);
                    setExtraGraphicSettings(elemTextBox, true);
                    finalMatrixTextBox[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
        }

        public bool isAllMatrixCalculated()
        {
            int indexI = 0, indexJ = 0, stopNumber = 0;
            TextBox[,] matrixTextBox;
            if (formNumber == 1)
            {
                indexI = rows;
                indexJ = columns;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox;
            } else if (formNumber == 2)
            {
                indexI = rows;
                indexJ = columns;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox1;
            } else if (formNumber == 3)
            {
                indexI = rows1;
                indexJ = columns1;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox1;
            } else
            {
                indexI = rows;
                indexJ = columns;
                stopNumber = 2;
                matrixTextBox = initialMatrixTextBox;
            }
            return checkMatrix(matrixTextBox, indexI, indexJ, stopNumber);
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
                                fillTextBoxByColor(initialMatrixTextBox[i, p],
                                    numberTextBox, finalMatrixTextBox[i, p], initialMatrixColor,
                                    numberColor, finalMatrixColor);
                                finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                                initialMatrixTextBox[i, p].Tag = 1;
                                return;
                            }
                            fillTextBoxByGray(initialMatrixTextBox[i, p],
                                finalMatrixTextBox[i, p]);
                        }
                        else if (formNumber == 2)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                            {
                                fillTextBoxByColor(initialMatrixTextBox1[i, p],
                                    initialMatrixTextBox2[i, p], finalMatrixTextBox[i, p],
                                    initialMatrix1Color, initialMatrix2Color, finalMatrixColor);
                                finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                                initialMatrixTextBox1[i, p].Tag = 1;
                                return;
                            }
                            fillTextBoxByGray(initialMatrixTextBox1[i, p],
                                initialMatrixTextBox2[i, p], finalMatrixTextBox[i, p]);
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
                                fillTextBoxByColor(initialMatrixTextBox1[i, k],
                                    initialMatrixTextBox2[k, p], initialMatrix1Color, initialMatrix2Color);

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
                            fillTextBoxByGray(initialMatrixTextBox1[i, k],
                                initialMatrixTextBox2[k, p]);
                        }
                        fillTextBoxDependingOnIndexes(i, p);
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
                    fillTextBoxByGray(initialMatrixTextBox[rows - 1, columns - 1],
                        finalMatrixTextBox[rows - 1, columns - 1], numberTextBox);
                    
                } else if (formNumber == 2)
                {
                    fillTextBoxByGray(initialMatrixTextBox1[rows - 1, columns - 1],
                        initialMatrixTextBox2[rows - 1, columns - 1],
                        finalMatrixTextBox[rows - 1, columns - 1]);
                } else if (formNumber == 3)
                {
                    fillTextBoxByGray(initialMatrixTextBox1[rows1 - 1, columns1 - 1],
                        initialMatrixTextBox2[rows2 - 1, columns2 - 1]);
                    fillTextBoxDependingOnIndexes();
                    
                    finalMatrixTextBox[rows1 - 1, columns2 - 1].Background = Brushes.Gray;
                    finalMatrixTextBox[rows1 - 1, columns2 - 1].Text = finalMatrix.nums[rows1 - 1, columns2 - 1].ToString();
                } else if (formNumber == 4)
                {
                    if (rows != 2)
                    {
                        initialMatrixTextBox[rows - 1, columns - 1].Background = Brushes.Gray;
                    } else
                    {
                        initialMatrixTextBox[rows - 1, columns - 2].Background = Brushes.Gray;
                    }
                    
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
        public void activateTimer(Label speedLightLabel)
        {
            timer.Tick += timerTick;

            var interval = Convert.ToInt32(speedLightLabel.Content);
            timer.Interval = new TimeSpan(0, 0, interval);
            timer.Start();

            manageButtons(false);
        }

        public void closeForm(Window window)
        {
            var actions = new ActionsWindow();
            actions.Show();
            window.Close();
        }
    }
}
