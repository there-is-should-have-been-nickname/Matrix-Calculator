using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            if (assistant.canParseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
                multiplicationFormRows2, multiplicationFormColumns2))
            {
                assistant.parseRowsAndColumns(multiplicationFormRows1, multiplicationFormColumns1,
                multiplicationFormRows2, multiplicationFormColumns2);

                if (assistant.isSuitableSizes())
                {
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
                else
                {
                    MessageBox.Show("Количество столбцов первой матрицы не равно количеству строк второй матрицы. Так умножать матрицы нельзя. Пожалуйста, сделайте их равными и попробуйте еще раз");
                }
                
                
            }
            else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
        }

        private void multiplicationFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox1();

            if (assistant.canParseInitialMatrixesTextBox() 
                && assistant.canDefineColors(multiplicationFormInitialMatrix1Color, 
                multiplicationFormInitialMatrix2Color, multiplciationFormFinalMatrixColor))
            {

                assistant.initialMatrix1 = assistant.getInitialMatrix1();
                assistant.initialMatrix2 = assistant.getInitialMatrix2();

                assistant.finalMatrix = assistant.initialMatrix1.multiplication(assistant.initialMatrix2);
                
                assistant.creationAndInsertionFinalMatrix();
                assistant.activateTimer(multiplicationFormButtonCreate, 
                    multiplicationFormButtonCalculate, multiplcaitionFormButtonBack, 
                    multiplicationFormSpeedLightLabel);
            }
            else
            {
                MessageBox.Show("Число или один (или более) элементов матрицы не являются числом. Возможно, вы не выбрали оператор, который будет применен к матрицам. Пожалуйста, исправьте это и попробуйте снова");
            }
        }

        private void multiplicationFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationFormSpeedLightLabel.Content = Math.Round(multiplicationFormSpeedLight.Value);
        }

        private void multiplicationFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var actions = new ActionsWindow();
            actions.Show();
            Close();
        }
    }
}
