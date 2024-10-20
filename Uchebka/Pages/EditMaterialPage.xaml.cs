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
    /// Логика взаимодействия для EditMaterialPage.xaml
    /// </summary>
    public partial class EditMaterialPage : Page
    {
        public Material material;
        public EditMaterialPage(Material _mat)
        {
            InitializeComponent();
            material = _mat;
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MaterialPage());
        }
    }
}
