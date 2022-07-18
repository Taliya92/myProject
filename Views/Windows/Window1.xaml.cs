using myProject.Views.Windows;
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

namespace myProject
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();//создаем новый объект окна
            this.Hide();//скрывает текущее окно LoginWindow
            regWindow.ShowDialog();//открываем новое окно в режиме диалога
            this.Show();//отображаем окно LoginWindow
        }
    }
}
