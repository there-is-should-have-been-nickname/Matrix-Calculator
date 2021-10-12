using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        private readonly Assistant assistant;
        public AdditionWindow()
        {
            InitializeComponent();
            assistant = new Assistant(2, additionFormButtonCreate, additionFormButtonCalculate,
                additionFormButtonBack, additionFormInitialMatrix1Color,
                additionFormInitialMatrix2Color, additionFormFinalMatrixColor);
        }
        private void AdditionFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.CanParseRowsAndColumns(additionFormRows,
                additionFormColumns))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова", "Сообщение");
                return;
            }

            assistant.EnableColorsAndSpeed(additionFormInitialMatrix1Color, additionFormInitialMatrix2Color,
            additionFormFinalMatrixColor, additionFormSpeedLight, additionFormButtonCalculate);


            assistant.ParseRowsAndColumns(additionFormRows,
                additionFormColumns);
            assistant.SetHeightWindow(this);
            assistant.CreationAndInsertionInnerGrid(additionFormGrid, this);
            assistant.CreationAndInsertionInitialMatrixTextBox1();
            assistant.CreationAndInsertionInitialMatrixTextBox2();
            assistant.CreationAndInsertionEqualLabel();
        }

        private void AdditionFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.ClearInitialMatrixTextBox1();

            if (!assistant.CanParseInitialMatrixesTextBox())
            {
                MessageBox.Show("Один (или более) элементов матриц не являются числом. Пожалуйста, внесите значения и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.CanParseOperator(additionFormOperator))
            {
                MessageBox.Show("Вы не выбрали знак операции. Пожалуйста, исправьте это и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.CanDefineColors(additionFormInitialMatrix1Color,
                    additionFormInitialMatrix2Color,
                    additionFormFinalMatrixColor))
            {
                MessageBox.Show("Вы не указали цвета, которыми будут подсвечиваться элементы. Пожалуйста, исправьте это и попробуйте снова", "Сообщение");
                return;
            }

            assistant.CreationAndInsertionOperatorLabel(additionFormOperator);

            assistant.initialMatrix1 = assistant.GetInitialMatrix1();
            assistant.initialMatrix2 = assistant.GetInitialMatrix2();

            if (additionFormOperator.Text == "+")
            {
                assistant.finalMatrix = assistant.initialMatrix1.Addition(assistant.initialMatrix2);
            } else {
                assistant.finalMatrix = assistant.initialMatrix1.Substraction(assistant.initialMatrix2);
            }
                
            assistant.CreationAndInsertionFinalMatrix();
            assistant.ActivateTimer(additionFormSpeedLightLabel);
        }

        private void AdditionFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            additionFormSpeedLightLabel.Content = Math.Round(additionFormSpeedLight.Value);
        }

        private void AdditionFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.CloseForm(this);
        }

    }
}
