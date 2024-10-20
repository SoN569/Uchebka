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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthButt_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTbx.Text == "" || PasswordPbx.Password == "")
                MessageBox.Show("Логин или пароль пустые!", "Ошибка авторизации");

            else if (App.db.User.Where(x => x.Login == LoginTbx.Text && x.Password == PasswordPbx.Password).Count() > 0)
            {
                User user = App.db.User.First(x => x.Login == LoginTbx.Text && x.Password == PasswordPbx.Password);
                App.user = user;
                MessageBox.Show("Вы успешно вошли!", "Успешная авторизация");

                NavigationService.Navigate(new MainPage());
            }
            else MessageBox.Show("Логин или пароль некорректны!", "Ошибка авторизации");
        }

        private void RegButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
