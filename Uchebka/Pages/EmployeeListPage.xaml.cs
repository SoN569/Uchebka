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
    /// Логика взаимодействия для EmployeeListPage.xaml
    /// </summary>
    public partial class EmployeeListPage : Page
    {
        public EmployeeListPage()
        {
            InitializeComponent();
            EmployeeLV.ItemsSource = App.db.User.Where(x => x.RoleId != 4).ToList();
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage(new Components.User()));
        }

        private void RedactButt_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeLV.SelectedItem != null)
            {
                User emp = (User)EmployeeLV.SelectedItem;
                NavigationService.Navigate(new EmployeePage(emp));
            }
            else MessageBox.Show("Выберите сотрудника для редактирования!", "Ошибка редактирования");
        }
    }
}
