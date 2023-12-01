using AuthorizationPage.Databases;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AuthorizationPage.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ToLoginButton_Click(object sender, RoutedEventArgs e)
        { 
            StringBuilder err = new StringBuilder();

            if (LoginBox.Text == "")
                err.AppendLine("Логин пуст");
            if (PasswordBox.Password == "")
                err.AppendLine("Пароль пуст");

            if (err.Length > 0)
                MessageBox.Show(err.ToString());
            else
            {
                LogPas user = App.db.LogPas.Where(x => x.Login == LoginBox.Text && x.Password == PasswordBox.Password).FirstOrDefault();
                if (user != null)
                    MessageBox.Show($"Вы успешно зашли, {user.Login} ({user.Worker_type.Title})");
                else
                    MessageBox.Show("Что-то неверно ;)");
            }
        }

        private void ToRegistButton_Click(object sender, RoutedEventArgs e)
        {
            App.mainWindow.MainFrame.Navigate(new Registration());
            App.mainWindow.PageTitle.Text = "Регистрация";
        }
    }
}
