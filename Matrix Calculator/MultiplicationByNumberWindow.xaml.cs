using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MultiplicationByNumberWindow.xaml
    /// </summary>
    public partial class MultiplicationByNumberWindow : Window
    {
        private Assistant assistant;

        public MultiplicationByNumberWindow()
        {
            InitializeComponent();
            assistant = new Assistant(1, multiplicationByNumberFormButtonCreate,
                multiplicationByNumberFormButtonCalculate,
                multiplicationByNumberFormButtonBack, multiplicationByNumberFormInitialMatrixColor,
                multiplicationByNumberFormNumberColor, multiplicationByNumberFormFinalMatrixColor);   
        }
        private void multiplicationByNumberFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.canParseRowsAndColumns(multiplicationByNumberFormRows,
                multiplicationByNumberFormColumns))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, " +
                    "выберите одно из значений и попробуйте снова", "Сообщение");
                return;
            }
            
            assistant.enableColorsAndSpeed(multiplicationByNumberFormInitialMatrixColor,
            multiplicationByNumberFormNumberColor,
            multiplicationByNumberFormFinalMatrixColor,
            multiplicationByNumberFormSpeedLight,
            multiplicationByNumberFormButtonCalculate);

            assistant.parseRowsAndColumns(multiplicationByNumberFormRows,
                multiplicationByNumberFormColumns);
            assistant.setHeightWindow(this);
            assistant.creationAndInsertionInnerGrid(multiplicationByNumberFormGrid, this);
            assistant.creationAndInsertionInitialMatrixTextBox();
            assistant.creationAndInsertionOperatorLabel();
            assistant.creationAndInsertionNumberTextBox();
            assistant.creationAndInsertionEqualLabel();
        }

        private void multiplicationByNumberFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox();
            
            if (!assistant.canParseInitialMatrixTextBox())
            {
                MessageBox.Show("Один (или более) элементов матрицы не являются числом. Пожалуйста, " +
                    "внесите значения и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.canParseNumberTextBox())
            {
                MessageBox.Show("Число, на которое вы собираетесь умножать, не является числом. Пожалуйста, " +
                   "внесите значение и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.canDefineColors(multiplicationByNumberFormInitialMatrixColor,
                    multiplicationByNumberFormNumberColor,
                    multiplicationByNumberFormFinalMatrixColor))
            {
                MessageBox.Show("Не указаны цвета, которыми будут подсвечиваться элементы", "Сообщение");
                return;
            }

            assistant.initialMatrix = assistant.getInitialMatrix();
            assistant.number = Convert.ToInt32(assistant.numberTextBox.Text);

            assistant.finalMatrix = assistant.initialMatrix.multiplicationOnNumber(assistant.number);
            assistant.creationAndInsertionFinalMatrix();
            assistant.activateTimer(multiplicationByNumberFormSpeedLightLabel);
        }

        private void multiplicationByNumberFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationByNumberFormSpeedLightLabel.Content = Math.Round(multiplicationByNumberFormSpeedLight.Value);
        }

        private void multiplicationByNumberFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.closeForm(this);
        }
    }
}
