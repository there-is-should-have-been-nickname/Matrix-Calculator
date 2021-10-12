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
    /// Логика взаимодействия для helpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            helpWindowTextBox.Text = "Уважаемый пользователь, вашему вниманию представлен " +
                "визуализатор матричных вычислений. В этом приложении наглядно показывается " +
                "как матрицы умножаются на число, складываются и вычитаются, умножаются друг " +
                "на друга, а также как высчитывается определитель путем подсвечивания текущих " +
                "элементов, над которыми совершается операция.\nЧерез главное окно вы можете " +
                "перейти к доступным операциям, зайти в раздел \"Помощь\"(что вы уже и сделали), " +
                "а также выйти из программы. В окне каждой операции вверху располагается " +
                "простой интерфейс, где вы можете выбрать соответствующие характеристики" +
                " матриц(ы), цвет подсветки элементов при визуализации. Над элементами " +
                "интерфейса, отвечающими за параметры отображения, находятся надписи, " +
                "которые показывают, за какой конкретно параметр они отвечают. " +
                "Если вы задали недостаточное количество параметров для выполнения какого - " +
                "либо действия(создать / посчитать) или задали их неправильно, то вы увидите " +
                "окно с текстом, объясняющим ошибку.\nСрок действия не ограничен и " +
                "использовать приложение можно сколько угодно. Надеемся, мы смогли " +
                "помочь вам лучше разобраться в элементарных матричных операциях." +
                " Приятного пользования!";
        }

        private void HelpFormButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.ActivateOperationsButton();
            Close();
        }
    }
}
