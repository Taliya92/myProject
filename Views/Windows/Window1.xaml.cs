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
using System.IO;
using System.Text.Json;
using myProject.Models;

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
            //1. проверить существует файл users.json
            //2. считать файл
            //3. привод json к list
            //4. проверить существование пользователя
            //5. если существует, осуществить переход на MainWindow
            string path = $"{Environment.CurrentDirectory}/users.json";
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                List<User> users = JsonSerializer.Deserialize<List<User>>(data);
                User currentUser = users.FirstOrDefault(c=>c.UserName==LoginBox.Text&&
                c.Password==PasswordBox.Text);
                if (currentUser!=null)
                {
                    MainWindow mainWindow = new MainWindow();
                    this.Hide();
                    mainWindow.ShowDialog();
                    this.Show();
                }

            }



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
