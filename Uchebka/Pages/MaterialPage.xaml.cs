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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uchebka.Components;

namespace Uchebka.Pages
{
    /// <summary>
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        public MaterialPage()
        {
            InitializeComponent();
            List<Warehouse> list = App.db.Warehouse.ToList();
            list.Add(new Warehouse()
            {
                Id = 4,
                Name = "Все склады",
            }) ;
            WarehouseCbx.ItemsSource = list;
            WarehouseCbx.DisplayMemberPath = "Name";

            if ("124".Contains(App.user.RoleId.ToString()))
            {
                DeleteButt.Visibility = Visibility.Collapsed;
                RedactButt.Visibility = Visibility.Collapsed;
            }

            Refresh();
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void WarehouseCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            int pos = 0;
            int count = 0;
            decimal sum = 0;
            MaterialLV.Items.Clear();
            if (WarehouseCbx.SelectedIndex >= 0 && WarehouseCbx.SelectedIndex < 3)
            {
                int id = WarehouseCbx.SelectedIndex + 1;
                foreach (var material in App.db.Material.Where(x => x.IdWarehouse == id).ToList())
                {
                    MaterialLV.Items.Add(material);
                    pos++;
                    count += (int)material.Count;
                    if (material.PriceOneKg != null) sum += (decimal)material.PriceOneKg;
                }
            }
            else
            {
                foreach (var material in App.db.Material.ToList())
                {
                    MaterialLV.Items.Add(material);
                    pos ++;
                    count += (int)material.Count;
                    if (material.PriceOneKg != null) sum += (decimal)material.PriceOneKg;
                }
            }
            PositionsTb.Text = pos.ToString();
            MaterialAmountTb.Text = count.ToString();
            SumTb.Text = sum.ToString();
        }

        private void DeleteButt_Click(object sender, RoutedEventArgs e)
        {
            var material = MaterialLV.SelectedItem;
            if (material == null) MessageBox.Show("Выберите материал для удаления!", "Ошибка удаления");
            else
            {
                Components.Material mat = (Components.Material)material;
                if (mat.Count > 0) MessageBox.Show("Невозможно удалить позицию, если оставшееся количество материала больше 0!", "Ошибка удаления");
                else
                {
                    MessageBoxResult msg = MessageBox.Show("Вы уверены, что хотите удалить данную позицию?", "Подтверждение удаления", MessageBoxButton.OKCancel);
                    switch (msg)
                    {
                        case MessageBoxResult.Cancel:
                            break;
                        case MessageBoxResult.OK:
                            App.db.Material.Remove(mat);
                            App.db.SaveChanges();
                            MessageBox.Show("Удалении позиции было успешно", "Успешное удаление");
                            Refresh();
                            break;
                    }
                }
            }
        }

        private void RedactButt_Click(object sender, RoutedEventArgs e)
        {
            var material = MaterialLV.SelectedItem;
            if (material == null) MessageBox.Show("Выберите материал для редактирования!", "Ошибка редактирования");
            else
            {
                Components.Material mat = (Components.Material)material;
                NavigationService.Navigate(new EditMaterialPage(mat));
            }
        }
    }
}
