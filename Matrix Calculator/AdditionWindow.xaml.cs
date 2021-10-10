using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для AdditionWindow.xaml
    /// </summary>
    public partial class AdditionWindow : Window
    {
        private Assistant assistant; 
        public AdditionWindow()
        {
            InitializeComponent();
            assistant = new Assistant(2, additionFormButtonCreate, additionFormButtonCalculate,
                additionFormButtonBack, additionFormInitialMatrix1Color,
                additionFormInitialMatrix2Color, additionFormFinalMatrixColor);
        }
        private void additionFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.canParseRowsAndColumns(additionFormRows,
                additionFormColumns))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
                return;
            }

            assistant.enableColorsAndSpeed(additionFormInitialMatrix1Color, additionFormInitialMatrix2Color,
            additionFormFinalMatrixColor, additionFormSpeedLight, additionFormButtonCalculate);


            assistant.parseRowsAndColumns(additionFormRows,
                additionFormColumns);
            assistant.setHeightWindow(this);
            assistant.creationAndInsertionInnerGrid(additionFormGrid, this);
            assistant.creationAndInsertionInitialMatrixTextBox1();
            assistant.creationAndInsertionInitialMatrixTextBox2();
            assistant.creationAndInsertionEqualLabel();
        }

        private void additionFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox1();

            if (!assistant.canParseInitialMatrixesTextBox())
            {
                MessageBox.Show("Один (или более) элементов матриц не являются числом. Пожалуйста, внесите значения и попробуйте снова");
                return;
            }

            if (!assistant.canDefineColors(additionFormInitialMatrix1Color,
                    additionFormInitialMatrix2Color,
                    additionFormFinalMatrixColor))
            {
                MessageBox.Show("Вы не выбрали знак операции. Пожалуйста, исправьте это и попробуйте снова");
                return;
            }

            if (!assistant.canParseOperator(additionFormOperator))
            {
                MessageBox.Show("Вы не указали цвета, которыми будут подсвечиваться элементы. Пожалуйста, исправьте это и попробуйте снова");
                return;
            }

            assistant.creationAndInsertionOperatorLabel(additionFormOperator);

            assistant.initialMatrix1 = assistant.getInitialMatrix1();
            assistant.initialMatrix2 = assistant.getInitialMatrix2();

            if (additionFormOperator.Text == "+")
            {
                assistant.finalMatrix = assistant.initialMatrix1.addition(assistant.initialMatrix2);
            } else {
                assistant.finalMatrix = assistant.initialMatrix1.substraction(assistant.initialMatrix2);
            }
                
            assistant.creationAndInsertionFinalMatrix();
            assistant.activateTimer(additionFormSpeedLightLabel);
        }

        private void additionFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            additionFormSpeedLightLabel.Content = Math.Round(additionFormSpeedLight.Value);
        }

        private void additionFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.closeForm(this);
        }

    }
}
