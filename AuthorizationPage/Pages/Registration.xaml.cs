using AuthorizationPage.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();
            Regex phone = new Regex(@"^(\+7|8)(\s|\(|)\d{3}(\s|\)|)(\s|)\d{3}(\s|-|)\d{2}(\s|-|)\d{2}$");
            Regex email = new Regex(@"^\S@(gmail.com|mail.ru)$");

            if (SurnameBox.Text == "")
                err.AppendLine("Фамилия пуста");
            if (NameBox.Text == "")
                err.AppendLine("Имя пусто");
            if (PatronymicBox.Text == "")
                err.AppendLine("Отчество пусто");
            if (PhoneBox.Text == "")
                err.AppendLine("Телефон пуст");
            else if (!phone.IsMatch(PhoneBox.Text))
                err.AppendLine("Неверный формат телефона");
            if (EmailBox.Text == "")
                err.AppendLine("Почта пуста");
            else if (!email.IsMatch(EmailBox.Text))
                err.AppendLine("Неверная почта");
            if (BirthdayDate.SelectedDate == null)
                err.AppendLine("Дата пуста");
            if (MaleRadio.IsChecked == false && FemaleRadio.IsChecked == false)
                err.AppendLine("Пол не выбран");
            if (LoginBox.Text == "")
                err.AppendLine("Логин пуст");
            if (PasswordBox.Password == "")
                err.AppendLine("Пароль пуст");
            else if (PasswordBox.Password.Length < 6 || !PasswordBox.Password.Any(char.IsUpper) || !PasswordBox.Password.Any(char.IsDigit) || PasswordBox.Password.IndexOfAny("!@#$%^".ToCharArray()) == -1)
                err.AppendLine("Неверный формат пароля");
            if (App.db.LogPas.Where(x => x.Login == LoginBox.Text).FirstOrDefault() != null)
                err.AppendLine("Такой логин уже существует");


            if (err.Length > 0)
                MessageBox.Show(err.ToString());
            else
            {
                App.db.LogPas.Add(new LogPas()
                {
                    Login = LoginBox.Text,
                    Password = PasswordBox.Password,
                    Id_worker = 1
                });
                App.db.SaveChanges();

                string gender;
                if (MaleRadio.IsChecked == true)
                    gender = "М";
                else
                    gender = "Ж";

                App.db.Clients.Add(new Clients()
                {
                    Surname = SurnameBox.Text,
                    Name = NameBox.Text,
                    Patronymic = PatronymicBox.Text,
                    Phone = PhoneBox.Text,
                    Email = EmailBox.Text,
                    Gender = gender,
                    Birthday = BirthdayDate.SelectedDate,
                    Id_LogPas = App.db.LogPas.OrderByDescending(x => x.Id).First().Id
                });
                App.db.SaveChanges();

                App.mainWindow.MainFrame.Navigate(new Authorization());
                App.mainWindow.PageTitle.Text = "Авторизация";
                MessageBox.Show("Вы успешно зарегистрировались");
            }
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
