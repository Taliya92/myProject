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
using Word = Microsoft.Office.Interop.Word;

namespace myProject.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        User user;
        public EditUser()
        {
            InitializeComponent();
        }


        public EditUser(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = user;
            //LoginBox.Text=user.UserName;
            //PasswordBox.Text=user.Password;
        }                    

    }
}
