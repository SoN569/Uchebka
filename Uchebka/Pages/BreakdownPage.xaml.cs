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
    /// Логика взаимодействия для BreakdownPage.xaml
    /// </summary>
    public partial class BreakdownPage : Page
    {
        public BreakdownPage()
        {
            InitializeComponent();
            BreakdownLV.ItemsSource = App.db.BreakdownEq.ToList();
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditBreakdownPage(new Components.BreakdownEq()));
        }

        private void RedButt_Click(object sender, RoutedEventArgs e)
        {
            if (BreakdownLV.SelectedItem != null)
            {
                BreakdownEq breakdown = BreakdownLV.SelectedItem as BreakdownEq;
                if (breakdown.TimeEnd == null)
                {
                    breakdown.TimeEnd = DateTime.Now;
                    App.db.SaveChanges();
                    NavigationService.Navigate(new BreakdownPage());
                }
                else MessageBox.Show("Сбой уже имеет конечное время!", "Ошибка редактирования");
            }
            else MessageBox.Show("Не выбран сбой для редактирования!", "Ошибка редактирования");
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
