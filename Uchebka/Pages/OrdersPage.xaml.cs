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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            switch (App.user.RoleId)
            {
                case 1:
                    //RoleTb.Text = "Экран мастера";
                    AddButt.Visibility = Visibility.Collapsed;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Collapsed;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.IdStatus == 6 || x.IdStatus == 7).ToList();
                    break;

                case 2:
                    //RoleTb.Text = "Экран конструктора";
                    AddButt.Visibility = Visibility.Collapsed;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Collapsed;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.IdStatus == 3).ToList();
                    break;

                case 3:
                    //RoleTb.Text = "Экран директора";
                    AddButt.Visibility = Visibility.Collapsed;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Collapsed;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.ToList();
                    break;

                case 4:
                   // RoleTb.Text = "Экран заказчика";
                    AddButt.Visibility = Visibility.Visible;
                    DeleteButt.Visibility = Visibility.Visible;
                    CancelButt.Visibility = Visibility.Visible;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Visible;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.LoginCustomer ==  App.user.Login).ToList();
                    break;

                case 5:
                    // RoleTb.Text = "Экран менеджера";
                    AddButt.Visibility = Visibility.Visible;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Visible;
                    AcceptButt.Visibility = Visibility.Visible;
                    RedactButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.LoginManager == App.user.Login || x.IdStatus == 1).ToList();
                    break;
            }
        }

        private void StatusCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            List<Order> orders = App.db.Order.ToList();

            switch (StatusCbx.SelectedIndex)
            {
                case 0:
                    orders = orders.Where(x => x.IdStatus != 2 && x.IdStatus < 5).ToList();
                    break;

                case 1:
                    orders = orders.Where(x => x.IdStatus >= 8).ToList();
                    break; 
                
                case 2:
                    orders = orders.Where(x => x.IdStatus > 4 && x.IdStatus < 8).ToList();
                    break;
                
                case 3:
                    orders = orders.Where(x => x.IdStatus == 2).ToList();
                    break;
            }

            switch (App.user.RoleId)
            {
                case 1:
                    //RoleTb.Text = "Экран мастера";
                    orders = orders.Where(x => x.IdStatus == 6 || x.IdStatus == 7).ToList();
                    break;

                case 2:
                    //RoleTb.Text = "Экран конструктора";
                    orders = orders.Where(x => x.IdStatus == 3).ToList();
                    break;

                case 4:
                    // RoleTb.Text = "Экран заказчика";
                    orders = orders.Where(x => x.LoginCustomer == App.user.Login).ToList();
                    break;

                case 5:
                    // RoleTb.Text = "Экран менеджера";
                    orders = orders.Where(x => x.LoginManager == App.user.Login || x.IdStatus == 1).ToList();
                    break;
            }

            OrdersLV.ItemsSource = orders;
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrdersPage(new Order()));
        }

        private void DeleteButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedactButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для редактирования!", "Ошибка редактирования");
            else if (sel.IdStatus != 1) MessageBox.Show("Вы не можете редактировать заказ, статус которого не «Новый»!", "Ошибка редактирования");
            else NavigationService.Navigate(new EditOrdersPage(sel));
        }
    }
}
