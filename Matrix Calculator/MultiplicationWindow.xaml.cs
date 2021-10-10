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
        private Assistant assistant;
        
        public MultiplicationWindow()
        {
            InitializeComponent();
            assistant = new Assistant(3, multiplicationFormButtonCreate,
                multiplicationFormButtonCalculate, multiplcaitionFormButtonBack,
                multiplicationFormInitialMatrix1Color, multiplicationFormInitialMatrix2Color,
                multiplciationFormFinalMatrixColor);
        }

        private void multiplicationFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.canParseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
                multiplicationFormRows2, multiplicationFormColumns2))
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
                return;
            }
            
            assistant.parseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
            multiplicationFormRows2, multiplicationFormColumns2);

            if (!assistant.isSuitableSizes())
            {
                MessageBox.Show("Число столбцов первой матрицы не равно числу строк второй матрицы. Пожалуйста, измените значения и попробуйте снова");
                return;
            }

            assistant.enableColorsAndSpeed(multiplicationFormInitialMatrix1Color,
                multiplicationFormInitialMatrix2Color, multiplciationFormFinalMatrixColor,
                multiplicationFormSpeedLight, multiplicationFormButtonCalculate);
            assistant.setHeightWindow(this);
            assistant.creationAndInsertionInnerGrid(multiplicationFormGrid, this);
            assistant.creationAndInsertionInitialMatrixTextBox1();
            assistant.creationAndInsertionOperatorLabel();
            assistant.creationAndInsertionInitialMatrixTextBox2();
            assistant.creationAndInsertionEqualLabel();
        }

        private void multiplicationFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox1();
            
            if (!assistant.canParseInitialMatrixesTextBox())
            {
                MessageBox.Show("Число или один (или более) элементов матриц не являются числом. Пожалуйста, внесите значения и попробуйте снова");
                return;
            } 

            if (!assistant.canDefineColors(multiplicationFormInitialMatrix1Color,
                multiplicationFormInitialMatrix2Color, multiplciationFormFinalMatrixColor))
            {
                MessageBox.Show("Вы не выбрали цвета подсветки элементов. Пожалуйста, исправьте это и попробуйте снова");
                return;
            }

            assistant.initialMatrix1 = assistant.getInitialMatrix1();
            assistant.initialMatrix2 = assistant.getInitialMatrix2();

            assistant.finalMatrix = assistant.initialMatrix1.multiplication(assistant.initialMatrix2);
                
            assistant.creationAndInsertionFinalMatrix();
            assistant.activateTimer(multiplicationFormSpeedLightLabel);
        }

        private void multiplicationFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationFormSpeedLightLabel.Content = Math.Round(multiplicationFormSpeedLight.Value);
        }

        private void multiplicationFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.closeForm(this);
        }
    }
}
