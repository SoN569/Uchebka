using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для EditBreakdownPage.xaml
    /// </summary>
    public partial class EditBreakdownPage : Page
    {
        public BreakdownEq breakdown;
        public EditBreakdownPage(BreakdownEq beq)
        {
            breakdown = beq;
            InitializeComponent();
            EquipmentCbx.ItemsSource = App.db.Equipment.ToList();
            EquipmentCbx.DisplayMemberPath = "Model";

            ReasonCbx.ItemsSource = App.db.ReasonBreakdown.ToList();
            ReasonCbx.DisplayMemberPath = "Name";

            StartDP.SelectedDate = DateTime.Today;
            HourTbx.Text = DateTime.Now.Hour.ToString();
            MinuteTbx.Text = DateTime.Now.Minute.ToString();
            
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BreakdownPage());
        }

        private void SaveButt_Click(object sender, RoutedEventArgs e)
        {
            if (EquipmentCbx.SelectedIndex == -1 || ReasonCbx.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбраны оборудование или причина сбоя!", "Ошибка сохранения");
            }
            else
            {
                breakdown.IdReason = ReasonCbx.SelectedIndex + 1;
                breakdown.IdEquipment = EquipmentCbx.Text;
                DateTime date = new DateTime();
             
                date = date.AddYears(StartDP.SelectedDate.Value.Year - 1);
                date = date.AddMonths(StartDP.SelectedDate.Value.Month - 1);
                date = date.AddDays(StartDP.SelectedDate.Value.Day - 1);
                date = date.AddHours(int.Parse(HourTbx.Text));
                date = date.AddMinutes(int.Parse(MinuteTbx.Text));
                breakdown.TimeStart = date;

                App.db.BreakdownEq.Add(breakdown);
                App.db.SaveChanges();
                NavigationService.Navigate(new BreakdownPage());
            }
        }
    }
}
