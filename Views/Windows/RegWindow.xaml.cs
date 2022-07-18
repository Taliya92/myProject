using myProject.Models;
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
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace myProject.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try//обработчик иcключительных ситуаций
            {
                if (PasswordBox.Password == RePasswordBox.Password)
                {
                    string messageTestPassword = Utils.Utils.TestPassword(PasswordBox.Password);
                    if (Utils.Utils.TestPassword(PasswordBox.Password)!="OK")
                    {
                        throw new Exception(messageTestPassword);
                    }
                    User newUser = new User();

                    newUser.UserName = LoginBox.Text;
                    newUser.Password = PasswordBox.Password;

                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                        WriteIndented = true
                    };

                    string path = $"{Environment.CurrentDirectory}/users.json";
                    List<User> users = new List<User>();

                    if (File.Exists(path))
                    {
                        string data = File.ReadAllText(path);
                        users = JsonSerializer.Deserialize<List<User>>(data);
                        if (users.FirstOrDefault(c => c.UserName == newUser.UserName) != null)
                        {
                            throw new Exception("Пользователь существует!");
                        }
                        else
                        {

                            users.Add(newUser);
                            data = JsonSerializer.Serialize<List<User>>(users,options);
                            File.WriteAllText(path, data);
                        }
                    }
                    else
                    {
                        users.Add(newUser);
                        string data = JsonSerializer.Serialize<List<User>>(users);
                        File.WriteAllText(path, data);
                    }
                    DialogResult = true;

                   // string data=JsonSerializer.Serialize<User>(newUser, options);
                   // File.WriteAllText($"{Environment.CurrentDirectory}/users.json",data);


                  //  MessageBox.Show($"{newUser.UserName}-{newUser.Password}");
                }
                else
                {
                    // MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception("Пароли не совпадают");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
