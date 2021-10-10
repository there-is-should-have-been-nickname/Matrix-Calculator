using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для DeterminantWindow.xaml
    /// </summary>
    public partial class DeterminantWindow : Window
    {
        private Assistant assistant;
        public DeterminantWindow()
        {
            InitializeComponent();
            assistant = new Assistant(4, determinantFormButtonCreate, determinantFormButtonCalculate,
                determinantFormButtonBack, determinantFormInitialMatrixColor);
        }

        private void determinantFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.canParseOrder(determinantFormOrder))
            {
                MessageBox.Show("Вы не выбрали порядок матрицы. Пожалуйста, выберите одно из значений и попробуйте снова");
                return;
            }
            
            assistant.parseOrder(determinantFormOrder);
            assistant.enableColorsAndSpeed(determinantFormInitialMatrixColor,
                determinantFormSpeedLight, determinantFormButtonCalculate);
            assistant.setHeightWindow(this);
            assistant.creationAndInsertionInnerGrid(determinantFormGrid, this);
            assistant.creationAndInsertionInitialMatrixTextBox();
            assistant.creationAndInsertionDeterminantTextBox();
        }

        private void determinantFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox();

            if (!assistant.canParseInitialMatrixTextBox())
            {
                MessageBox.Show("Один (или более) элементов матрицы не являются числом. Пожалуйста, исправьте это и попробуйте снова");
                return;
            }

            if (!assistant.canDefineColors(determinantFormInitialMatrixColor))
            {
                MessageBox.Show("Вы не указали цвет подсветки. Пожалуйста, исправьте это и попробуйте снова");
                return;
            }

            assistant.initialMatrix = assistant.getInitialMatrix();
            assistant.determinant = assistant.initialMatrix.getDeterminant();
            assistant.activateTimer(determinantFormSpeedLightLabel);
        }

        private void determinantFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            determinantFormSpeedLightLabel.Content = Math.Round(determinantFormSpeedLight.Value);
        }

        private void determinantFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.closeForm(this);
        }
    }
}
