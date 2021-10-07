using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            
            if (assistant.canParseRowsAndColumns(additionFormRows,
                additionFormColumns))
            {
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
            else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
        }

        private void additionFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox1();

            if (assistant.canParseInitialMatrixesTextBox()
                && assistant.canDefineColors(additionFormInitialMatrix1Color,
                    additionFormInitialMatrix2Color,
                    additionFormFinalMatrixColor) && assistant.canParseOperator(additionFormOperator))
            {

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
            else
            {
                MessageBox.Show("Число или один (или более) элементов матрицы не являются числом. Или вы не выбрали знак операции.Возможно, что вы не указали цвета, которыми будут подсвечиваться элементы. Пожалуйста, исправьте это и попробуйте снова");
            }
        }

        private void additionFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            additionFormSpeedLightLabel.Content = Math.Round(additionFormSpeedLight.Value);
        }

        private void additionFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var actions = new ActionsWindow();
            actions.Show();
            Close();
        }

    }
}
