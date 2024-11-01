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
                    StatusButt.Visibility= Visibility.Visible;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.IdStatus == 6 || x.IdStatus == 7).ToList();
                    break;

                case 2:
                    //RoleTb.Text = "Экран конструктора";
                    AddButt.Visibility = Visibility.Collapsed;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Collapsed;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Visible;
                    StatusButt.Visibility = Visibility.Visible;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.IdStatus == 3).ToList();
                    break;

                case 3:
                    //RoleTb.Text = "Экран директора";
                    AddButt.Visibility = Visibility.Collapsed;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Collapsed;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Collapsed;
                    StatusButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.ToList();
                    break;

                case 4:
                   // RoleTb.Text = "Экран заказчика";
                    AddButt.Visibility = Visibility.Visible;
                    DeleteButt.Visibility = Visibility.Visible;
                    CancelButt.Visibility = Visibility.Visible;
                    AcceptButt.Visibility = Visibility.Collapsed;
                    RedactButt.Visibility = Visibility.Visible;
                    StatusButt.Visibility = Visibility.Collapsed;

                    OrdersLV.ItemsSource = App.db.Order.Where(x => x.LoginCustomer ==  App.user.Login).ToList();
                    break;

                case 5:
                    // RoleTb.Text = "Экран менеджера";
                    AddButt.Visibility = Visibility.Visible;
                    DeleteButt.Visibility = Visibility.Collapsed;
                    CancelButt.Visibility = Visibility.Visible;
                    AcceptButt.Visibility = Visibility.Visible;
                    RedactButt.Visibility = Visibility.Collapsed;
                    StatusButt.Visibility = Visibility.Visible;

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
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для удаления!", "Ошибка удаления");
            else
            {
                if (App.user.RoleId == 4 && sel.IdStatus == 1)
                {
                    App.db.Order.Remove(sel);
                    App.db.SaveChanges();
                    MessageBox.Show("Вы успешно удалили заказ!", "Успешное удаление");
                    Refresh();
                }
                else MessageBox.Show("Невозможно удалить заказ!", "Ошибка удаления");
            }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для отклонения!", "Ошибка отклонения");
            else
            {
                if (App.user.RoleId == 5)
                {
                    if (sel.IdStatus == 1 || sel.IdStatus == 4)
                    {
                        sel.IdStatus = 2;
                        MessageBox.Show("Вы успешно отклонили заказ!", "Успешная отмена");
                    }
                    else MessageBox.Show("Невозможно отклонить заказ с данным статусом!", "Ошибка отмены заказа");
                }
                else if (App.user.RoleId == 4)
                {
                    if (sel.IdStatus < 5 && sel.IdStatus > 2)
                    {
                        sel.IdStatus = 2;
                        MessageBox.Show("Вы успешно отклонили заказ!", "Успешная отмена");
                    }
                    else MessageBox.Show("Невозможно отклонить заказ с данным статусом!", "Ошибка отмены заказа");
                }
                App.db.SaveChanges();
                Refresh();
            }
        }

        private void AcceptButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для принятия!", "Ошибка редактирования");
            else if (sel.IdStatus != 1) MessageBox.Show("Невозможно принять заказ, если его статус не «Новый»!", "Ошибка редактирования");
            else
            {
                sel.IdStatus = 3;
                sel.LoginManager = App.user.Login;
                App.db.SaveChanges();
                MessageBox.Show("Вы успешно приняли заказ!", "Успешное редактирование");
            }
            Refresh();
        }

        private void RedactButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для редактирования!", "Ошибка редактирования");
            else
            {
                if (App.user.RoleId == 4)
                {
                    if (sel.IdStatus != 1) MessageBox.Show("Вы не можете редактировать заказ, статус которого не «Новый»!", "Ошибка редактирования");
                    else NavigationService.Navigate(new EditOrdersPage(sel));
                }
                if (App.user.RoleId == 2)
                {
                    NavigationService.Navigate(new EditOrdersPage(sel));
                }
            }
            
        }

        private void StatusButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = OrdersLV.SelectedItem as Order;
            if (sel == null) MessageBox.Show("Не выбран заказ для изменения статуса!", "Ошибка изменения статуса");
            else
            {
                if (App.user.RoleId == 5)
                {
                    if (sel.IdStatus == 4)
                    {
                        sel.IdStatus = 5;
                        App.db.SaveChanges();
                        MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                        Refresh();
                    }
                    else if (sel.IdStatus == 5)
                    {
                        sel.IdStatus = 6;
                        App.db.SaveChanges();
                        MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                        Refresh();
                    }
                    if (sel.IdStatus == 8)
                    {
                        sel.IdStatus = 9;
                        App.db.SaveChanges();
                        MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                        Refresh();
                    }
                }
                else if (App.user.RoleId == 2)
                {
                    if (sel.Price != null && sel.DateEnd != null)
                    {
                        sel.IdStatus = 4;
                        App.db.SaveChanges();
                        MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                        Refresh();
                    }
                    else MessageBox.Show("Вы не можете изменить статус заказа, у которого нет плановой даты и стоимости!", "Ошибка изменения статуса");
                    
                }
                else if (App.user.RoleId == 1)
                {
                    if (sel.IdStatus == 6)
                    {
                        sel.IdStatus = 7;
                        App.db.SaveChanges();
                        MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                        Refresh();
                    }
                    else if (sel.IdStatus == 7)
                    {
                        if (App.db.Quality.Where(x => x.IdOrder == sel.Id && x.Examination == "-").Count() > 0) MessageBox.Show("Невозможно изменить статус при отрицательной оценке качества!", "Ошибка изменения статуса");
                        else
                        {
                            sel.IdStatus = 8;
                            App.db.SaveChanges();
                            MessageBox.Show("Вы успешно поменяли статус заказа!", "Успешное изменение статуса");
                            Refresh();
                        }
                    }
                }
            }
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
