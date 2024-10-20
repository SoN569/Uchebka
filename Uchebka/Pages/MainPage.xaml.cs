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

namespace Uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            switch(App.user.RoleId)
            {
                case 1:
                    RoleTb.Text = "Экран мастера";
                    break;

                case 2:
                    RoleTb.Text = "Экран конструктора";
                    break;

                case 3:
                    RoleTb.Text = "Экран директора";
                    EmployeeListButt.Visibility = Visibility.Visible;
                    break;

                case 4:
                    RoleTb.Text = "Экран заказчика";
                    MaterialListButt.Visibility = Visibility.Collapsed;
                    ComponentsListButt.Visibility = Visibility.Collapsed;
                    break;

                case 5:
                    RoleTb.Text = "Экран менеджера";
                    break;
            }
        }

        private void EmployeeListButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeListPage());
        }

        private void MaterialListButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MaterialPage());
        }

        private void ComponentsListButt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
