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

            UnitCbx.ItemsSource = App.db.Unit.ToList();
            UnitCbx.DisplayMemberPath = "Name";

            SupplierCbx.ItemsSource = App.db.Supplier.ToList();
            SupplierCbx.DisplayMemberPath = "SupplierName";

            TypeCbx.ItemsSource = App.db.TypeMaterial.ToList();
            TypeCbx.DisplayMemberPath = "Name";

            StandartCbx.ItemsSource = App.db.Standart.ToList();
            StandartCbx.DisplayMemberPath = "Name";

            WarehouseCbx.ItemsSource = App.db.Warehouse.ToList();
            WarehouseCbx.DisplayMemberPath = "Name";

            if (material.Article != null )
            {
                ArticleTbx.Text = material.Article;
                NameTbx.Text = material.Name;
                CountTbx.Text = material.Count.ToString();
                PriceTbx.Text = material.PriceOneKg.ToString();
                WidthTbx.Text = material.WidthMetr.ToString();
                MassTbx.Text = material.MassOneMetr.ToString();

                if (material.MatImage != null)
                {
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = new MemoryStream(material.MatImage);
                    imageSource.EndInit();
                    MaterialImg.Source = imageSource;
                }
                UnitCbx.SelectedIndex = (int)material.IdUnit - 1;
                SupplierCbx.Text = material.SupplierName;
                TypeCbx.SelectedIndex = (int)material.IdTypeMaterial - 1;
                if (material.IdStandart != null) StandartCbx.SelectedIndex = (int)material.IdStandart - 1;
                if (material.IdWarehouse != null) WarehouseCbx.SelectedIndex = (int)material.IdWarehouse - 1;
            }
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(CountTbx.Text) < 0 || double.Parse(PriceTbx.Text) < 0 || double.Parse(WidthTbx.Text) < 0 || double.Parse(MassTbx.Text) < 0)
                {
                    MessageBox.Show("Числа не могут быть отрицательными!", "Ошибка редактирования материала");
                }
                else
                {
                    material.Name = NameTbx.Text;
                    material.Count = int.Parse(CountTbx.Text);
                    material.PriceOneKg = (decimal?)double.Parse(PriceTbx.Text);
                    material.WidthMetr = (decimal?)double.Parse(WidthTbx.Text);
                    material.MassOneMetr = (decimal?)double.Parse(MassTbx.Text);
                    
                    material.IdUnit = UnitCbx.SelectedIndex + 1;
                    material.SupplierName = SupplierCbx.Text;
                    material.IdTypeMaterial = TypeCbx.SelectedIndex + 1;
                    if (StandartCbx.SelectedIndex != -1) material.IdStandart = StandartCbx.SelectedIndex + 1;
                    if (WarehouseCbx.SelectedIndex != -1) material.IdWarehouse = WarehouseCbx.SelectedIndex + 1;
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
            NavigationService.Navigate(new MaterialPage());
        }

        private void DeleteImgButt_Click(object sender, RoutedEventArgs e)
        {
            material.MatImage = null;
            MaterialImg.Source = null;
        }

        private void ImageButt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jepg|*.jepg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                material.MatImage = File.ReadAllBytes(openFile.FileName);
                MaterialImg.Source = new BitmapImage(new Uri(openFile.FileName));
                
            }
            else MessageBox.Show("Изображение не было выбрано!", "Ошибка изображения");
        }
    }
}
