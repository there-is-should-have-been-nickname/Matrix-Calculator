using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MultiplicationWindow.xaml
    /// </summary>
    /// 

    public partial class MultiplicationWindow : Window
    {
        private readonly Assistant assistant;
        
        public MultiplicationWindow()
        {
            InitializeComponent();
            assistant = new Assistant(3, multiplicationFormButtonCreate,
                multiplicationFormButtonCalculate, multiplcaitionFormButtonBack,
                multiplicationFormInitialMatrix1Color, multiplicationFormInitialMatrix2Color,
                multiplciationFormFinalMatrixColor);
        }

        private void MultiplicationFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.CanParseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
                multiplicationFormRows2, multiplicationFormColumns2))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова", "Сообщение");
                return;
            }
            
            assistant.ParseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
            multiplicationFormRows2, multiplicationFormColumns2);

            if (!assistant.IsSuitableSizes())
            {
                MessageBox.Show("Число столбцов первой матрицы не равно числу строк второй матрицы. Пожалуйста, измените значения и попробуйте снова", "Сообщение");
                return;
            }

            assistant.EnableColorsAndSpeed(multiplicationFormInitialMatrix1Color,
                multiplicationFormInitialMatrix2Color, multiplciationFormFinalMatrixColor,
                multiplicationFormSpeedLight, multiplicationFormButtonCalculate);
            assistant.SetHeightWindow(this);
            assistant.CreationAndInsertionInnerGrid(multiplicationFormGrid, this);
            assistant.CreationAndInsertionInitialMatrixTextBox1();
            assistant.CreationAndInsertionOperatorLabel();
            assistant.CreationAndInsertionInitialMatrixTextBox2();
            assistant.CreationAndInsertionEqualLabel();
        }

        private void MultiplicationFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.ClearInitialMatrixTextBox1();
            
            if (!assistant.CanParseInitialMatrixesTextBox())
            {
                MessageBox.Show("Число или один (или более) элементов матриц не являются числом. Пожалуйста, внесите значения и попробуйте снова", "Сообщение");
                return;
            } 

            if (!assistant.CanDefineColors(multiplicationFormInitialMatrix1Color,
                multiplicationFormInitialMatrix2Color, multiplciationFormFinalMatrixColor))
            {
                MessageBox.Show("Вы не выбрали цвета подсветки элементов. Пожалуйста, исправьте это и попробуйте снова", "Сообщение");
                return;
            }

            assistant.initialMatrix1 = assistant.GetInitialMatrix1();
            assistant.initialMatrix2 = assistant.GetInitialMatrix2();

            assistant.finalMatrix = assistant.initialMatrix1.Multiplication(assistant.initialMatrix2);
                
            assistant.CreationAndInsertionFinalMatrix();
            assistant.ActivateTimer(multiplicationFormSpeedLightLabel);
        }

        private void MultiplicationFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationFormSpeedLightLabel.Content = Math.Round(multiplicationFormSpeedLight.Value);
        }

        private void MultiplicationFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.CloseForm(this);
        }
    }
}
