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
using Uchebka.Components;

namespace Uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void RegButt_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            if (NameTbx.Text == "" || SurnameTbx.Text == "" || LoginTbx.Text == "" || PasswordPbx.Password == "")
            {
                error = true;
                MessageBox.Show("Вы не заполнили все обязательные поля!", "Ошибка регистрации");
            }
            else if (PasswordPbx.Password.Length < 4 || PasswordPbx.Password.Length > 16)
            {
                error = true;
                MessageBox.Show("Пароль должен содержать от 4 до 16 символов!", "Ошибка регистрации");
            }
            bool IsUpper = false;
            bool IsNumber = false;
            bool IsSymbol = false;
            foreach (char letter in PasswordPbx.Password)
            {
                if (char.IsNumber(letter)) IsNumber = true;
                if (char.IsUpper(letter)) IsUpper = true;
                if ("*&{}|+".Contains(letter)) IsSymbol = true;
            }
            if (!IsUpper || !IsNumber || IsSymbol)
            {
                error = true;
                MessageBox.Show("Пароль не подходит под условия!", "Ошибка регистрации");
            }
            if (!error)
            {
                User user = new User()
                {
                    Login = LoginTbx.Text,
                    Password = PasswordPbx.Password,
                    FirstName = NameTbx.Text,
                    LastName = SurnameTbx.Text,
                    RoleId = 4,
                };
                if (BirthDP.SelectedDate != null) user.DateBithday = BirthDP.SelectedDate;
                if (PatronymicTbx.Text != "") user.Patronymic = PatronymicTbx.Text;

                try
                {
                    App.db.User.Add(user);
                    App.db.SaveChanges();
                    MessageBox.Show("Регистрация прошла успешно!", "Успешная регистрация");
                    App.user = user;
                    NavigationService.Navigate(new MainPage());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка регистрации");
                }
            }
        }
    }
}
