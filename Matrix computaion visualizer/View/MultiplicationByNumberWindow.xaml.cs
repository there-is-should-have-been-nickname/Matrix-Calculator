using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MultiplicationByNumberWindow.xaml
    /// </summary>
    public partial class MultiplicationByNumberWindow : Window
    {
        private readonly Assistant assistant;

        public MultiplicationByNumberWindow()
        {
            InitializeComponent();
            assistant = new Assistant(1, multiplicationByNumberFormButtonCreate,
                multiplicationByNumberFormButtonCalculate,
                multiplicationByNumberFormButtonBack, multiplicationByNumberFormInitialMatrixColor,
                multiplicationByNumberFormNumberColor, multiplicationByNumberFormFinalMatrixColor);   
        }
        private void MultiplicationByNumberFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.CanParseRowsAndColumns(multiplicationByNumberFormRows,
                multiplicationByNumberFormColumns))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, " +
                    "выберите одно из значений и попробуйте снова", "Сообщение");
                return;
            }
            
            assistant.EnableColorsAndSpeed(multiplicationByNumberFormInitialMatrixColor,
            multiplicationByNumberFormNumberColor,
            multiplicationByNumberFormFinalMatrixColor,
            multiplicationByNumberFormSpeedLight,
            multiplicationByNumberFormButtonCalculate);

            assistant.ParseRowsAndColumns(multiplicationByNumberFormRows,
                multiplicationByNumberFormColumns);
            assistant.SetHeightWindow(this);
            assistant.CreationAndInsertionInnerGrid(multiplicationByNumberFormGrid, this);
            assistant.CreationAndInsertionInitialMatrixTextBox();
            assistant.CreationAndInsertionOperatorLabel();
            assistant.CreationAndInsertionNumberTextBox();
            assistant.CreationAndInsertionEqualLabel();
        }

        private void MultiplicationByNumberFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.ClearInitialMatrixTextBox();
            
            if (!assistant.CanParseInitialMatrixTextBox())
            {
                MessageBox.Show("Один (или более) элементов матрицы не являются числом. Пожалуйста, " +
                    "внесите значения и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.CanParseNumberTextBox())
            {
                MessageBox.Show("Число, на которое вы собираетесь умножать, не является числом. Пожалуйста, " +
                   "внесите значение и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.CanDefineColors(multiplicationByNumberFormInitialMatrixColor,
                    multiplicationByNumberFormNumberColor,
                    multiplicationByNumberFormFinalMatrixColor))
            {
                MessageBox.Show("Не указаны цвета, которыми будут подсвечиваться элементы", "Сообщение");
                return;
            }

            assistant.initialMatrix = assistant.GetInitialMatrix();
            assistant.Number = Convert.ToInt32(assistant.numberTextBox.Text);

            assistant.finalMatrix = assistant.initialMatrix.MultiplicationOnNumber(assistant.Number);
            assistant.CreationAndInsertionFinalMatrix();
            assistant.ActivateTimer(multiplicationByNumberFormSpeedLightLabel);
        }

        private void MultiplicationByNumberFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationByNumberFormSpeedLightLabel.Content = Math.Round(multiplicationByNumberFormSpeedLight.Value);
        }

        private void MultiplicationByNumberFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.CloseForm(this);
        }
    }
}
