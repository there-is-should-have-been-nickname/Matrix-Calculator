using System;
using System.Windows;
namespace Matrix_Calculator
{
    /// <summary>
    /// Логика взаимодействия для DeterminantWindow.xaml
    /// </summary>
    public partial class DeterminantWindow : Window
    {
        private readonly Assistant assistant;
        public DeterminantWindow()
        {
            InitializeComponent();
            assistant = new Assistant(4, determinantFormButtonCreate, determinantFormButtonCalculate,
                determinantFormButtonBack, determinantFormInitialMatrixColor);
        }

        private void DeterminantFormButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!assistant.CanParseOrder(determinantFormOrder))
            {
                MessageBox.Show("Вы не выбрали порядок матрицы. Пожалуйста, выберите одно из значений и попробуйте снова", "Сообщение");
                return;
            }
            
            assistant.ParseOrder(determinantFormOrder);
            assistant.EnableColorsAndSpeed(determinantFormInitialMatrixColor,
                determinantFormSpeedLight, determinantFormButtonCalculate);
            assistant.SetHeightWindow(this);
            assistant.CreationAndInsertionInnerGrid(determinantFormGrid, this);
            assistant.CreationAndInsertionInitialMatrixTextBox();
        }

        private void DeterminantFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.ClearInitialMatrixTextBox();

            if (!assistant.CanParseInitialMatrixTextBox())
            {
                MessageBox.Show("Один (или более) элементов матрицы не являются числом. Пожалуйста, исправьте это и попробуйте снова", "Сообщение");
                return;
            }

            if (!assistant.CanDefineColors(determinantFormInitialMatrixColor))
            {
                MessageBox.Show("Вы не указали цвет подсветки. Пожалуйста, исправьте это и попробуйте снова", "Сообщение");
                return;
            }

            assistant.initialMatrix = assistant.GetInitialMatrix();
            assistant.CreationAndInsertionDeterminantTextBox();
            assistant.Determinant = assistant.initialMatrix.GetDeterminant();
            assistant.ActivateTimer(determinantFormSpeedLightLabel);
        }

        private void DeterminantFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            determinantFormSpeedLightLabel.Content = Math.Round(determinantFormSpeedLight.Value);
        }

        private void DeterminantFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            assistant.CloseForm(this);
        }
    }
}
