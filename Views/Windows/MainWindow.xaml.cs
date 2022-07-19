using myProject.Models;
using myProject.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace myProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        string path = $"{Environment.CurrentDirectory}/users.json";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //1 проверить существует файл users.json
            //2 считать файл
            //3 привод json к list
            //4 присвоить свойству ItemsSource наш list
           

            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                users = JsonSerializer.Deserialize<ObservableCollection<User>>(data);
                UserGrid.ItemsSource = users;

            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = UserGrid.SelectedItem as User;
            EditUser editUser = new EditUser(selectedItem);
            editUser.ShowDialog();
            string data = JsonSerializer.Serialize<ObservableCollection<User>>(users);
            File.WriteAllText(path, data);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = UserGrid.SelectedItem as User;
            users.Remove(selectedItem);
            string data = JsonSerializer.Serialize<ObservableCollection<User>>(users);
            File.WriteAllText(path,data);
        }

        private async void WordButton_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = UserGrid.SelectedItem as User;
            //await Print(selectedItem);

            foreach (var item in users)
            {
                await Print(item);
            }
        }
        async Task Print(User item)
        {
            await Task.Run(() =>
            {
                //создаем экземпляр word
                var word = new Word.Application();
               //открываем документ
                Word.Document document = word.Documents.Open(Environment.CurrentDirectory + @"\user_tamplate.docx");
                try
                {

                    //обращение к закладкам
                    document.Bookmarks["UserName"].Range.Text = item.UserName;
                    document.Bookmarks["Password"].Range.Text = item.Password;
                    //сохранение докумета
                    document.SaveAs2(Environment.CurrentDirectory + $"/{item.UserName}.docx");
                    //закрытие документа и приложения Word
                    document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                    word.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);

                }
                catch (Exception ex)
                {
                    document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                    word.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                }

            });
        }
    }
}
