using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditComponentPage.xaml
    /// </summary>
    public partial class EditComponentPage : Page
    {
        public Components.Components components;
        public EditComponentPage(Components.Components _com)
        {
            InitializeComponent();
            components = _com;

            UnitCbx.ItemsSource = App.db.Unit.ToList();
            UnitCbx.DisplayMemberPath = "Name";

            SupplierCbx.ItemsSource = App.db.Supplier.ToList();
            SupplierCbx.DisplayMemberPath = "SupplierName";

            TypeCbx.ItemsSource = App.db.TypeComponents.ToList();
            TypeCbx.DisplayMemberPath = "Name";

            WarehouseCbx.ItemsSource = App.db.Warehouse.ToList();
            WarehouseCbx.DisplayMemberPath = "Name";

            if (components.Article != null)
            {
                ArticleTbx.Text = components.Article;
                NameTbx.Text = components.Name;
                CountTbx.Text = components.Count.ToString();
                PriceTbx.Text = components.Price.ToString();
                MassTbx.Text = components.Weight.ToString();

                if (components.ComImage != null)
                {
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = new MemoryStream(components.ComImage);
                    imageSource.EndInit();
                    ComponentImg.Source = imageSource;
                }
                UnitCbx.SelectedIndex = (int)components.IdUnit - 1;
                SupplierCbx.Text = components.SupplierName;
                TypeCbx.SelectedIndex = (int)components.IdTypeComponents - 1;
                if (components.IdWarehouse != null) WarehouseCbx.SelectedIndex = (int)components.IdWarehouse - 1;
            }

        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(CountTbx.Text) < 0 || double.Parse(PriceTbx.Text) < 0 || double.Parse(MassTbx.Text) < 0)
                {
                    MessageBox.Show("Числа не могут быть отрицательными!", "Ошибка редактирования материала");
                }
                else
                {
                    components.Name = NameTbx.Text;
                    components.Count = int.Parse(CountTbx.Text);
                    components.Price = (decimal?)double.Parse(PriceTbx.Text);
                    components.Weight = (decimal?)double.Parse(MassTbx.Text);

                    components.IdUnit = UnitCbx.SelectedIndex + 1;
                    components.SupplierName = SupplierCbx.Text;
                    components.IdTypeComponents = TypeCbx.SelectedIndex + 1;
                    if (WarehouseCbx.SelectedIndex != -1) components.IdWarehouse = WarehouseCbx.SelectedIndex + 1;
                    App.db.SaveChanges();
                    MessageBox.Show("Успешное редактирование материала!", "Успешное редактирование");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка редактирования материала");
            }
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComponentPage());
        }

        private void DeleteImgButt_Click(object sender, RoutedEventArgs e)
        {
            components.ComImage = null;
            ComponentImg.Source = null;
        }

        private void ImageButt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jepg|*.jepg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                components.ComImage = File.ReadAllBytes(openFile.FileName);
                ComponentImg.Source = new BitmapImage(new Uri(openFile.FileName));

            }
            else MessageBox.Show("Изображение не было выбрано!", "Ошибка изображения");
        }
    }
}
