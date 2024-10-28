using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : Page
    {
        public ComponentPage()
        {
            InitializeComponent();
            List<Warehouse> list = App.db.Warehouse.ToList();
            list.Add(new Warehouse()
            {
                Id = 4,
                Name = "Все склады",
            });
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
            ComponentsLV.Items.Clear();
            if (WarehouseCbx.SelectedIndex >= 0 && WarehouseCbx.SelectedIndex < 3)
            {
                int id = WarehouseCbx.SelectedIndex + 1;
                foreach (var component in App.db.Components.Where(x => x.IdWarehouse == id).ToList())
                {
                    ComponentsLV.Items.Add(component);
                    pos++;
                    count += (int)component.Count;
                    if (component.Price != null) sum += (decimal)component.Price;
                }
            }
            else
            {
                foreach (var component in App.db.Components.ToList())
                {
                    ComponentsLV.Items.Add(component);
                    pos++;
                    count += (int)component.Count;
                    if (component.Price != null) sum += (decimal)component.Price;
                }
            }
            PositionsTb.Text = pos.ToString();
            ComponentsAmountTb.Text = count.ToString();
            SumTb.Text = sum.ToString();
        }

        private void DeleteButt_Click(object sender, RoutedEventArgs e)
        {
            var component = ComponentsLV.SelectedItem;
            if (component == null) MessageBox.Show("Выберите комплектующие для удаления!", "Ошибка удаления");
            else
            {
                Components.Components com = (Components.Components)component;
                if (com.Count > 0) MessageBox.Show("Невозможно удалить позицию, если оставшееся количество комплектующих больше 0!", "Ошибка удаления");
                else
                {
                    MessageBoxResult msg = MessageBox.Show("Вы уверены, что хотите удалить данную позицию?", "Подтверждение удаления", MessageBoxButton.OKCancel);
                    switch (msg)
                    {
                        case MessageBoxResult.Cancel:
                            break;
                        case MessageBoxResult.OK:
                            App.db.Components.Remove(com);
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
            var component = ComponentsLV.SelectedItem;
            if (component == null) MessageBox.Show("Выберите материал для редактирования!", "Ошибка редактирования");
            else
            {
                Components.Components com = (Components.Components)component;
                NavigationService.Navigate(new EditComponentPage(com));
            }
        }
    }
}
