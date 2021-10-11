using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Matrix_Calculator
{
    public class Assistant
    {
        private int FormNumber { get; set; }
        private bool IsNextLine { get; set; } = false;
        private SolidColorBrush BackgroundColor { get; set; } = Brushes.Transparent;
        private SolidColorBrush BorderColor { get; set; } = Brushes.Black;

        private DispatcherTimer Timer { get; set; } = new DispatcherTimer();

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

        /// <summary>
        /// Parameters for calculation
        /// </summary>
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int Rows1 { get; set; }
        public int Columns1 { get; set; }
        public int Rows2 { get; set; }
        public int Columns2 { get; set; }
        public int Number { get; set; }
        public int Determinant { get; set; }
        
        public Matrix initialMatrix;
        public Matrix initialMatrix1;
        public Matrix initialMatrix2;
        public Matrix finalMatrix;

        private void SetNumberAndButtons(int formNumber, Button buttonCreate, Button buttonCalculate,
            Button buttonBack)
        {
            FormNumber = formNumber;
            this.buttonCreate = buttonCreate;
            this.buttonCalculate = buttonCalculate;
            this.buttonBack = buttonBack;
        }

        private void SetInitialGraphicSettings(Grid grid, Window window, int marginTop)
        {
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Width = window.Width - 50 - 50;
            grid.Background = BackgroundColor;
            grid.Margin = new Thickness(50, marginTop, 0, 0);
            grid.Height = window.Height - marginTop - 90;
        }

        private void SetInitialGraphicSettings(Control element, int fontSize, int marginLeft, int marginTop)
        {
            element.FontFamily = new FontFamily("Consolas");
            element.FontSize = fontSize;
            element.Width = 40;
            element.Height = 40;
            element.HorizontalContentAlignment = HorizontalAlignment.Center;
            element.VerticalContentAlignment = VerticalAlignment.Center;
            element.VerticalAlignment = VerticalAlignment.Top;
            element.HorizontalAlignment = HorizontalAlignment.Left;
            element.Background = BackgroundColor;
            element.BorderBrush = BorderColor;
            element.Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }

        private void SetExtraGraphicSettings(TextBox textbox, bool stateReadOnly = false)
        {
            textbox.Text = "";
            textbox.Background = BackgroundColor;
            textbox.BorderBrush = BorderColor;
            textbox.IsReadOnly = stateReadOnly;
        }

        private bool CheckMatrix(TextBox[,] matrixTextBox, int indI, int indJ, int stopNum)
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

        private void FillTextBoxDependingOnIndexes(int indI, int indJ)
        {
            finalMatrixTextBox[indI, indJ].Background = GetColor(finalMatrixColor);

            if (indI != 0 && indJ == 0)
            {
                finalMatrixTextBox[indI - 1, Columns2 - 1].Background = BackgroundColor;
            }
            else if (indI == Rows1 - 1 && indJ == Columns2 - 1)
            {
                finalMatrixTextBox[Rows1 - 1, Columns2 - 1].Background = BackgroundColor;
            }
            else if (indJ != 0)
            {
                finalMatrixTextBox[indI, indJ - 1].Background = BackgroundColor;
            }
        }

        private void FillTextBoxByColor(TextBox textBox1, TextBox textBox2, 
            TextBox textBox3, ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3)
        {
            textBox1.Background = GetColor(comboBox1);
            textBox2.Background = GetColor(comboBox2);
            textBox3.Background = GetColor(comboBox3);
        }
        private void FillTextBoxByColor(TextBox textBox1, TextBox textBox2,
            ComboBox comboBox1, ComboBox comboBox2)
        {
            textBox1.Background = GetColor(comboBox1);
            textBox2.Background = GetColor(comboBox2);
        }

        private void FillTextBoxByBackgroundColor(TextBox textBox1, TextBox textBox2,
            TextBox textBox3)
        {
            textBox1.Background = BackgroundColor;
            textBox2.Background = BackgroundColor;
            textBox3.Background = BackgroundColor;
        }
        private void FillTextBoxByBackgroundColor(TextBox textBox1, TextBox textBox2)
        {
            textBox1.Background = BackgroundColor;
            textBox2.Background = BackgroundColor;
        }

        private void FillTextBoxDependingOnIndexes()
        {
            if (Columns2 == 1 && Rows1 == 1)
            {
                finalMatrixTextBox[Rows1 - 1, Columns2 - 1].Background = BackgroundColor;
            }
            else if (Columns2 == 1 && Rows1 != 1)
            {
                finalMatrixTextBox[Rows1 - 2, Columns2 - 1].Background = BackgroundColor;
            }
            else
            {
                finalMatrixTextBox[Rows1 - 1, Columns2 - 2].Background = BackgroundColor;
            }
        }

        private void ManageMatrix(TextBox[,] mas, int indI, int indJ, bool state)
        {
            for (int i = 0; i < indI; ++i)
            {
                for (int p = 0; p < indJ; ++p)
                {
                    mas[i, p].IsReadOnly = state;
                }
            }
        }

        private void ManageNumberTextBox(bool state)
        {
            numberTextBox.IsReadOnly = state;
        }

        public Assistant()
        {
            
        }

        public Assistant(int formNumber, Button buttonCreate, Button buttonCalculate, 
            Button buttonBack, ComboBox initialMatrixColor, ComboBox numberColor, ComboBox finalMatrixColor)
        {
            SetNumberAndButtons(formNumber, buttonCreate, buttonCalculate, buttonBack);

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
            SetNumberAndButtons(formNumber, buttonCreate, buttonCalculate, buttonBack);
            this.initialMatrixColor = initialMatrixColor;
        }

        public void EnableColorsAndSpeed(ComboBox initialMatrixColor, 
            ComboBox numberColor, ComboBox finalMatrixColor, Slider speedLight, 
            Button buttonCalculate)
        {
            initialMatrixColor.IsEnabled = true;
            numberColor.IsEnabled = true;
            finalMatrixColor.IsEnabled = true;
            speedLight.IsEnabled = true;
            buttonCalculate.IsEnabled = true;
        }

        public void EnableColorsAndSpeed(ComboBox initialMatrixColor, Slider speedLight,
            Button buttonCalculate)
        {
            initialMatrixColor.IsEnabled = true;
            speedLight.IsEnabled = true;
            buttonCalculate.IsEnabled = true;
        }

        public bool CanParseRowsAndColumns(ComboBox rows, ComboBox columns)
        {
            return int.TryParse(rows.Text, out _) && int.TryParse(columns.Text, out _);
        }

        public bool CanParseRowsAndColumns(ComboBox rows1, ComboBox columns1, ComboBox rows2,
            ComboBox columns2)
        {
            return int.TryParse(rows1.Text, out _) 
                && int.TryParse(rows2.Text, out _) 
                && int.TryParse(columns1.Text, out _) 
                && int.TryParse(columns2.Text, out _);
        }

        public bool CanParseOperator(ComboBox operatorL)
        {
            return !string.IsNullOrWhiteSpace(operatorL.Text);
        }

        public bool CanParseOrder(ComboBox order)
        {
            return int.TryParse(order.Text, out _);
        }

        public void ParseRowsAndColumns(ComboBox rows, ComboBox columns)
        {
            Rows = Convert.ToInt32(rows.Text);
            Columns = Convert.ToInt32(columns.Text);
        }

        public void ParseRowsAndColumns(ComboBox rows1, ComboBox columns1, ComboBox rows2,
            ComboBox columns2)
        {
            Rows1 = Convert.ToInt32(rows1.Text);
            Columns1 = Convert.ToInt32(columns1.Text);
            Rows2 = Convert.ToInt32(rows2.Text);
            Columns2 = Convert.ToInt32(columns2.Text);
        }

        public void ParseOrder(ComboBox order)
        {
            Rows = Convert.ToInt32(order.Text);
            Columns = Rows;
        }

        public bool IsSuitableSizes()
        {
            return Columns1 == Rows2;
        }

        public void SetHeightWindow(Window window)
        {
            if (FormNumber == 1 || FormNumber == 4)
            {
                window.Height = 90 + Rows * 40 + 20 * 2 + (Rows - 1) * 10 + 90;
            } else if (FormNumber == 2)
            {
                window.Height = 150 + Rows * 40 + 20 * 2 + (Rows - 1) * 10 + 90;
            } else if (FormNumber == 3)
            {
                int biggerRow = (Rows1 >= Rows2) ? Rows1 : Rows2;
                window.Height = 130 + biggerRow * 40 + 20 * 2 + (biggerRow - 1) * 10 + 90;
            }
        }

        public void CreationAndInsertionInnerGrid(Grid grid, Window window)
        {
            if (grid.Children.Contains(innerGrid))
            {
                grid.Children.Remove(innerGrid);
            }

            innerGrid = new Grid();

            int marginTop = 0;

            if (FormNumber == 1 || FormNumber == 4)
            {
                marginTop = 90;
            }
            else if (FormNumber == 2)
            {
                marginTop = 150;
            }
            else if (FormNumber == 3)
            {
                marginTop = 130;
            }

            SetInitialGraphicSettings(innerGrid, window, marginTop);
            grid.Children.Add(innerGrid);
        }

        public void ClearInitialMatrixTextBox()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    initialMatrixTextBox[i, p].Background = BackgroundColor;
                    initialMatrixTextBox[i, p].Tag = 0;
                }
            }
        }

        public void ClearInitialMatrixTextBox1()
        {
            int indexI = 0, indexJ = 0;
            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
            } else if (FormNumber == 3)
            {
                indexI = Rows1;
                indexJ = Columns1;
            }

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    initialMatrixTextBox1[i, p].Background = BackgroundColor;
                    initialMatrixTextBox1[i, p].Tag = 0;
                }
            }
        }

        public void ClearInitialMatrixTextBox2()
        {
            int indexI = 0, indexJ = 0;

            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
            } else if (FormNumber == 3)
            {
                indexI = Rows2;
                indexJ = Columns2;
            }
            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    initialMatrixTextBox2[i, p].Background = BackgroundColor;
                    initialMatrixTextBox2[i, p].Tag = 0;
                }
            }
        }

        public void CreationAndInsertionInitialMatrixTextBox()
        {
            initialMatrixTextBox = new TextBox[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    var elemTextBox = new TextBox();
                    SetInitialGraphicSettings(elemTextBox, 18, 20 + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            ClearInitialMatrixTextBox();
        }

        public void CreationAndInsertionDeterminantTextBox()
        {
            determinantTextBox = new TextBox();
            SetInitialGraphicSettings(determinantTextBox, 18, Columns * 50 + 100, Rows * 50 / 2 - 5);
            SetExtraGraphicSettings(determinantTextBox, true);
            innerGrid.Children.Add(determinantTextBox);
        }

        public void CreationAndInsertionOperatorLabel()
        {
            var operatorLabel = new Label();

            int marginLeft = 0;
            if (FormNumber == 1)
            {
                marginLeft = 20 + Columns * 40 + (Columns - 1) * 10 + 10;
            } else if (FormNumber == 3)
            {
                marginLeft = 20 + Columns1 * 40 + (Columns1 - 1) * 10 + 10;
            }
            
            SetInitialGraphicSettings(operatorLabel, 14, marginLeft, (int)innerGrid.Height / 2 - 20);
            operatorLabel.Content = "*";
            innerGrid.Children.Add(operatorLabel);
        }

        public void CreationAndInsertionOperatorLabel(ComboBox operatorL)
        {
            var operatorLabel = new Label();
            SetInitialGraphicSettings(operatorLabel, 14, 20 + Columns * 40 + (Columns - 1) * 10 + 10, (int)innerGrid.Height / 2 - 20);
            operatorLabel.Content = operatorL.Text;
            innerGrid.Children.Add(operatorLabel);
        }

        public void CreationAndInsertionNumberTextBox()
        {
            numberTextBox = new TextBox();
            SetInitialGraphicSettings(numberTextBox, 18, 20 + Columns * 40 + (Columns - 1) * 10 + 60, (int)innerGrid.Height / 2 - 20);
            SetExtraGraphicSettings(numberTextBox);
            innerGrid.Children.Add(numberTextBox);
        }

        public void CreationAndInsertionInitialMatrixTextBox1()
        {
            int indexI = 0, indexJ = 0;
            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
            } else if (FormNumber == 3)
            {
                indexI = Rows1;
                indexJ = Columns1;
            }
            initialMatrixTextBox1 = new TextBox[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    SetInitialGraphicSettings(elemTextBox, 18, 20 + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox1[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            ClearInitialMatrixTextBox1();
        }

        public void CreationAndInsertionInitialMatrixTextBox2()
        {
            int indexI = 0, indexJ = 0, marginLeft = 0;

            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
                marginLeft = 20 + Columns * 40 + (Columns - 1) * 10 + 60;
            } else if (FormNumber == 3)
            {
                indexI = Rows2;
                indexJ = Columns2;
                marginLeft = 20 + Columns1 * 40 + (Columns1 - 1) * 10 + 60;
            }

            initialMatrixTextBox2 = new TextBox[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    SetInitialGraphicSettings(elemTextBox, 18, marginLeft + p * 50, 20 + i * 50);
                    elemTextBox.Text = "";
                    initialMatrixTextBox2[i, p] = elemTextBox;
                    innerGrid.Children.Add(elemTextBox);
                }
            }
            ClearInitialMatrixTextBox2();
        }
        public void CreationAndInsertionEqualLabel()
        {
            var operatorEqual = new Label();

            int marginLeft = 0;
            if (FormNumber == 1)
            {
                marginLeft = 20 + Columns * 40 + (Columns - 1) * 10 + 110;
            } else if (FormNumber == 2)
            {
                marginLeft = 20 + Columns * 40 * 2 + (Columns - 1) * 10 * 2 + 70;                
            } else if (FormNumber == 3)
            {
                marginLeft = 20 + Columns1 * 40 + (Columns1 - 1) * 10 + 70 + Columns2 * 40 + (Columns2 - 1) * 10;
            }
            SetInitialGraphicSettings(operatorEqual, 14, marginLeft, (int)innerGrid.Height / 2 - 20);
            operatorEqual.Content = "=";
            innerGrid.Children.Add(operatorEqual);
        }

        public bool CanParseInitialMatrixTextBox()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    bool isParsed = int.TryParse(initialMatrixTextBox[i, p].Text, out _);

                    if (!isParsed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanParseInitialMatrixesTextBox()
        {
            if (FormNumber == 2)
            {
                for (int i = 0; i < Rows; ++i)
                {
                    for (int p = 0; p < Columns; ++p)
                    {
                        bool isParsed1 = int.TryParse(initialMatrixTextBox1[i, p].Text, out _);
                        bool isParsed2 = int.TryParse(initialMatrixTextBox2[i, p].Text, out _);

                        if (!isParsed1 || !isParsed2)
                        {
                            return false;
                        }
                    }
                }
            } else if (FormNumber == 3)
            {
                for (int i = 0; i < Rows1; ++i)
                {
                    for (int p = 0; p < Columns1; ++p)
                    {
                        bool isParsed = int.TryParse(initialMatrixTextBox1[i, p].Text, out _);

                        if (!isParsed)
                        {
                            return false;
                        }
                    }
                }

                for (int i = 0; i < Rows2; ++i)
                {
                    for (int p = 0; p < Columns2; ++p)
                    {
                        bool isParsed = int.TryParse(initialMatrixTextBox2[i, p].Text, out _);

                        if (!isParsed)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CanParseNumberTextBox()
        {
            return int.TryParse(numberTextBox.Text, out _);
        }

        public bool CanDefineColors(ComboBox initialMatrixColor,
            ComboBox numberColor, ComboBox finalMatrixColor)
        {
            return !string.IsNullOrWhiteSpace(initialMatrixColor.Text)
                && !string.IsNullOrWhiteSpace(numberColor.Text)
                && !string.IsNullOrWhiteSpace(finalMatrixColor.Text);
        }

        public bool CanDefineColors(ComboBox initialMatrixColor)
        {
            return !string.IsNullOrWhiteSpace(initialMatrixColor.Text);
        }

        public Matrix GetInitialMatrix()
        {
            int[,] initialMatrixMas = new int[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    int elem = Convert.ToInt32(initialMatrixTextBox[i, p].Text);
                    initialMatrixMas[i, p] = elem;
                }
            }

            return new Matrix(Rows, Columns, initialMatrixMas);
        }
        public Matrix GetInitialMatrix1()
        {
            int indexI = 0, indexJ = 0;
            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
            } else if (FormNumber == 3)
            {
                indexI = Rows1;
                indexJ = Columns1;
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

        public Matrix GetInitialMatrix2()
        {
            int indexI = 0, indexJ = 0;
            if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
            } else if (FormNumber == 3)
            {
                indexI = Rows2;
                indexJ = Columns2;
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
        public void CreationAndInsertionFinalMatrix()
        {
            if (finalMatrixTextBox != null)
            {
                ClearFinalTextBox();
            }

            int indexI = 0, indexJ = 0, marginLeft = 0;
            if (FormNumber == 1)
            {
                indexI = Rows;
                indexJ = Columns;
                marginLeft = 20 + Columns * 40 + (Columns - 1) * 10 + 150 + 10;
            } else if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
                marginLeft = 20 + Columns * 40 * 2 + (Columns - 1) * 10 * 2 + 70 + 50;
            } else if (FormNumber == 3)
            {
                indexI = Rows1;
                indexJ = Columns2;
                marginLeft = 20 + Columns1 * 40 + (Columns1 - 1) * 10 + 70 + Columns2 * 40 + (Columns2 - 1) * 10 + 50;
            }
            finalMatrixTextBox = new TextBox[indexI, indexJ];

            for (int i = 0; i < indexI; ++i)
            {
                for (int p = 0; p < indexJ; ++p)
                {
                    var elemTextBox = new TextBox();
                    SetInitialGraphicSettings(elemTextBox, 18, marginLeft + p * 50, 20 + i * 50);
                    SetExtraGraphicSettings(elemTextBox, true);
                    finalMatrixTextBox[i, p] = elemTextBox;

                    innerGrid.Children.Add(elemTextBox);
                }
            }
        }
        public bool IsAllMatrixCalculated()
        {
            TextBox[,] matrixTextBox;
            int stopNumber, indexJ, indexI;
            if (FormNumber == 1)
            {
                indexI = Rows;
                indexJ = Columns;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox;
            }
            else if (FormNumber == 2)
            {
                indexI = Rows;
                indexJ = Columns;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox1;
            }
            else if (FormNumber == 3)
            {
                indexI = Rows1;
                indexJ = Columns1;
                stopNumber = 1;
                matrixTextBox = initialMatrixTextBox1;
            }
            else
            {
                indexI = Rows;
                indexJ = Columns;
                stopNumber = 2;
                matrixTextBox = initialMatrixTextBox;
            }
            return CheckMatrix(matrixTextBox, indexI, indexJ, stopNumber);
        }

        public SolidColorBrush GetColor(ComboBox comboBox)
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

        public void FillNextTextBox()
        {
            if (FormNumber == 1 || FormNumber == 2)
            {
                for (int i = 0; i < Rows; ++i)
                {
                    for (int p = 0; p < Columns; ++p)
                    {
                        if (FormNumber == 1)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox[i, p].Tag) == 0)
                            {
                                FillTextBoxByColor(initialMatrixTextBox[i, p],
                                    numberTextBox, finalMatrixTextBox[i, p], initialMatrixColor,
                                    numberColor, finalMatrixColor);
                                finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                                initialMatrixTextBox[i, p].Tag = 1;
                                return;
                            }
                            FillTextBoxByBackgroundColor(initialMatrixTextBox[i, p],
                                finalMatrixTextBox[i, p]);
                        }
                        else if (FormNumber == 2)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox1[i, p].Tag) == 0)
                            {
                                FillTextBoxByColor(initialMatrixTextBox1[i, p],
                                    initialMatrixTextBox2[i, p], finalMatrixTextBox[i, p],
                                    initialMatrix1Color, initialMatrix2Color, finalMatrixColor);
                                finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                                initialMatrixTextBox1[i, p].Tag = 1;
                                return;
                            }
                            FillTextBoxByBackgroundColor(initialMatrixTextBox1[i, p],
                                initialMatrixTextBox2[i, p], finalMatrixTextBox[i, p]);
                        }
                    }
                }
            } else if (FormNumber == 3)
            {
                for (int i = 0; i < Rows1; ++i)
                {
                    if (IsNextLine)
                    {
                        ClearInitialMatrixTextBox2();
                        IsNextLine = false;
                    }

                    for (int p = 0; p < Columns2; ++p)
                    {
                        for (int k = 0; k < Columns1; ++k)
                        {
                            if (Convert.ToInt32(initialMatrixTextBox1[i, k].Tag) == 0 && Convert.ToInt32(initialMatrixTextBox2[k, p].Tag) == 0)
                            {
                                FillTextBoxByColor(initialMatrixTextBox1[i, k],
                                    initialMatrixTextBox2[k, p], initialMatrix1Color, initialMatrix2Color);

                                if (p == Columns2 - 1)
                                {
                                    initialMatrixTextBox1[i, k].Tag = 1;
                                    if (k == Columns1 - 1)
                                    {
                                        IsNextLine = true;
                                    }
                                }


                                initialMatrixTextBox2[k, p].Tag = 1;
                                return;
                            }
                            FillTextBoxByBackgroundColor(initialMatrixTextBox1[i, k],
                                initialMatrixTextBox2[k, p]);
                        }
                        FillTextBoxDependingOnIndexes(i, p);
                        finalMatrixTextBox[i, p].Text = finalMatrix.nums[i, p].ToString();
                    }
                }
            }
        }

        public void FillNextTextBox(int order)
        {
            var color = GetColor(initialMatrixColor);
            if (order == 1)
            {
                initialMatrixTextBox[0, 0].Background = color;
                initialMatrixTextBox[0, 0].Tag = 2;
                return;
            }
            else if (order == 2)
            {
                for (int i = 0; i < Rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, i].Background = color;
                        initialMatrixTextBox[i, i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, i].Background = BackgroundColor;
                }

                for (int i = 0; i < Rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, Rows - 1 - i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, Rows - 1 - i].Background = color;
                        initialMatrixTextBox[i, Rows - 1 - i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, Rows - 1 - i].Background = BackgroundColor;

                }
            }
            else if (order == 3)
            {
                for (int i = 0; i < Rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, i].Tag) == 0)
                    {
                        initialMatrixTextBox[i, i].Background = color;
                        initialMatrixTextBox[i, i].Tag = 1;
                        return;
                    }
                    initialMatrixTextBox[i, i].Background = BackgroundColor;

                }

                if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 0)
                {
                    initialMatrixTextBox[1, 0].Background = color;
                    initialMatrixTextBox[1, 0].Tag = 1;
                    return;
                }
                initialMatrixTextBox[1, 0].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 0)
                {
                    initialMatrixTextBox[2, 1].Background = color;
                    initialMatrixTextBox[2, 1].Tag = 1;
                    return;
                }
                initialMatrixTextBox[2, 1].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[0, 2].Tag) == 0)
                {
                    initialMatrixTextBox[0, 2].Background = color;
                    initialMatrixTextBox[0, 2].Tag = 1;
                    return;
                }
                initialMatrixTextBox[0, 2].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 0)
                {
                    initialMatrixTextBox[0, 1].Background = color;
                    initialMatrixTextBox[0, 1].Tag = 1;
                    return;
                }
                initialMatrixTextBox[0, 1].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 0)
                {
                    initialMatrixTextBox[1, 2].Background = color;
                    initialMatrixTextBox[1, 2].Tag = 1;
                    return;
                }
                initialMatrixTextBox[1, 2].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[2, 0].Tag) == 0)
                {
                    initialMatrixTextBox[2, 0].Background = color;
                    initialMatrixTextBox[2, 0].Tag = 1;
                    return;
                }
                initialMatrixTextBox[2, 0].Background = BackgroundColor;

                for (int i = 0; i < Rows; ++i)
                {
                    if (Convert.ToInt32(initialMatrixTextBox[i, Rows - 1 - i].Tag) == 1)
                    {
                        initialMatrixTextBox[i, Rows - 1 - i].Background = color;
                        initialMatrixTextBox[i, Rows - 1 - i].Tag = 2;
                        return;
                    }
                    initialMatrixTextBox[i, Rows - 1 - i].Background = BackgroundColor;

                }

                if (Convert.ToInt32(initialMatrixTextBox[1, 2].Tag) == 1)
                {
                    initialMatrixTextBox[1, 2].Background = color;
                    initialMatrixTextBox[1, 2].Tag = 2;
                    return;
                }
                initialMatrixTextBox[1, 2].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[2, 1].Tag) == 1)
                {
                    initialMatrixTextBox[2, 1].Background = color;
                    initialMatrixTextBox[2, 1].Tag = 2;
                    return;
                }
                initialMatrixTextBox[2, 1].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[0, 0].Tag) == 1)
                {
                    initialMatrixTextBox[0, 0].Background = color;
                    initialMatrixTextBox[0, 0].Tag = 2;
                    return;
                }
                initialMatrixTextBox[0, 0].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[1, 0].Tag) == 1)
                {
                    initialMatrixTextBox[1, 0].Background = color;
                    initialMatrixTextBox[1, 0].Tag = 2;
                    return;
                }
                initialMatrixTextBox[1, 0].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[0, 1].Tag) == 1)
                {
                    initialMatrixTextBox[0, 1].Background = color;
                    initialMatrixTextBox[0, 1].Tag = 2;
                    return;
                }
                initialMatrixTextBox[0, 1].Background = BackgroundColor;

                if (Convert.ToInt32(initialMatrixTextBox[2, 2].Tag) == 1)
                {
                    initialMatrixTextBox[2, 2].Background = color;
                    initialMatrixTextBox[2, 2].Tag = 2;
                    return;
                }
            }
        }

        public void ManageButtons(bool state)
        {
            buttonCreate.IsEnabled = state;
            buttonCalculate.IsEnabled = state;
            buttonBack.IsEnabled = state;
        }
        public void ManageElements(bool state)
        {
            if (FormNumber == 1)
            {
                ManageMatrix(initialMatrixTextBox, Rows, Columns, state);
                ManageNumberTextBox(state);
            }
            else if (FormNumber == 2)
            {
                ManageMatrix(initialMatrixTextBox1, Rows, Columns, state);
                ManageMatrix(initialMatrixTextBox2, Rows, Columns, state);
            }
            else if (FormNumber == 3)
            {
                ManageMatrix(initialMatrixTextBox1, Rows1, Columns1, state);
                ManageMatrix(initialMatrixTextBox2, Rows2, Columns2, state);
            }
            else if (FormNumber == 4)
            {
                ManageMatrix(initialMatrixTextBox, Rows, Columns, state);
            }
        } 

        public void TimerTick(object sender, EventArgs e)
        {
            if (IsAllMatrixCalculated())
            {
                if (FormNumber == 1)
                {
                    FillTextBoxByBackgroundColor(initialMatrixTextBox[Rows - 1, Columns - 1],
                        finalMatrixTextBox[Rows - 1, Columns - 1], numberTextBox);
                    
                } else if (FormNumber == 2)
                {
                    FillTextBoxByBackgroundColor(initialMatrixTextBox1[Rows - 1, Columns - 1],
                        initialMatrixTextBox2[Rows - 1, Columns - 1],
                        finalMatrixTextBox[Rows - 1, Columns - 1]);
                } else if (FormNumber == 3)
                {
                    FillTextBoxByBackgroundColor(initialMatrixTextBox1[Rows1 - 1, Columns1 - 1],
                        initialMatrixTextBox2[Rows2 - 1, Columns2 - 1]);
                    FillTextBoxDependingOnIndexes();
                    
                    finalMatrixTextBox[Rows1 - 1, Columns2 - 1].Background = BackgroundColor;
                    finalMatrixTextBox[Rows1 - 1, Columns2 - 1].Text = finalMatrix.nums[Rows1 - 1, Columns2 - 1].ToString();
                } else if (FormNumber == 4)
                {
                    if (Rows != 2)
                    {
                        initialMatrixTextBox[Rows - 1, Columns - 1].Background = BackgroundColor;
                    } else
                    {
                        initialMatrixTextBox[Rows - 1, Columns - 2].Background = BackgroundColor;
                    }
                    
                    determinantTextBox.Text = Determinant.ToString();
                }

                Timer.Tick -= TimerTick;
                Timer.Stop();
                ManageButtons(true);
                ManageElements(false);
            }
            else
            {
                if (FormNumber < 4)
                {
                    FillNextTextBox();
                } else
                {
                    FillNextTextBox(Rows);
                }
            }
        }
        public void ActivateTimer(Label speedLightLabel)
        {
            Timer.Tick += TimerTick;

            var interval = Convert.ToInt32(speedLightLabel.Content);
            Timer.Interval = new TimeSpan(0, 0, interval);
            Timer.Start();

            ManageButtons(false);
            ManageElements(true);
        }

        public void CloseForm(Window window)
        {
            var actions = new ActionsWindow();
            actions.Show();
            window.Close();
        }
    }
}
