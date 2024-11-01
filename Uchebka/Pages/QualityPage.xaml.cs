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
    /// Логика взаимодействия для QualityPage.xaml
    /// </summary>
    public partial class QualityPage : Page
    {
        public QualityPage()
        {
            InitializeComponent();
            QualityLV.ItemsSource = App.db.Quality.ToList();
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditQualityPage(new Quality()));
        }

        private void RedactButt_Click(object sender, RoutedEventArgs e)
        {
            var sel = QualityLV.SelectedItem as Quality;
            if (sel == null) MessageBox.Show("Не выбрана оценка контроля для редактирования!", "Ошибка редактирования");
            else NavigationService.Navigate(new EditQualityPage(sel));
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
