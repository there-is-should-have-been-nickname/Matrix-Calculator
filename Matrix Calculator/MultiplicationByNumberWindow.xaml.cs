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
            
            if (assistant.canParseRowsAndColumns(multiplicationByNumberFormRows, 
                multiplicationByNumberFormColumns))
            {
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
            else
            {
                MessageBox.Show("Вы не выбрали число столбцов или строк. Пожалуйста, выберите одно из значений и попробуйте снова");
            }
            
        }

        private void multiplicationByNumberFormButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            assistant.clearInitialMatrixTextBox();
            
            if (assistant.canParseInitialMatrixTextBox() 
                && assistant.canParseNumberTextBox() 
                && assistant.canDefineColors(multiplicationByNumberFormInitialMatrixColor,
                    multiplicationByNumberFormNumberColor,
                    multiplicationByNumberFormFinalMatrixColor))
            {
                assistant.initialMatrix = assistant.getInitialMatrix();
                assistant.number = Convert.ToInt32(assistant.numberTextBox.Text);

                assistant.finalMatrix = assistant.initialMatrix.multiplicationOnNumber(assistant.number);
                assistant.creationAndInsertionFinalMatrix();
                assistant.activateTimer(multiplicationByNumberFormSpeedLightLabel);
            } else
            {
                MessageBox.Show("Число или один (или более) элементов матрицы не являются числом. Возможно, что вы не указали цвета, которыми будут подсвечиваться элементы. Пожалуйста, исправьте это и попробуйте снова");
            }
        }

        private void multiplicationByNumberFormSpeedLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            multiplicationByNumberFormSpeedLightLabel.Content = Math.Round(multiplicationByNumberFormSpeedLight.Value);
        }

        private void multiplicationByNumberFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var actions = new ActionsWindow();
            actions.Show();
            Close();
        }
    }
}
