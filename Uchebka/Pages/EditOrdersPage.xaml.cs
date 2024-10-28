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
    /// Логика взаимодействия для EditOrdersPage.xaml
    /// </summary>
    public partial class EditOrdersPage : Page
    {
        public Order order;
        public EditOrdersPage(Order ord)
        {
            InitializeComponent();
            order = ord;
            switch(App.user.RoleId)
            {
                case 5:
                    break;

                case 4:
                    CustomerSP.Visibility = Visibility.Collapsed;
                    CostTbx.IsReadOnly = true;
                    DateEndTbx.IsReadOnly = true;
                    break;
            }

            if (order.Id == null)
            {
                string id = "";
                if (App.user.LastName != null) id += App.user.LastName.ToUpper().ToArray()[0];
                else id += "_";
                if (App.user.FirstName != null) id += App.user.FirstName.ToUpper().ToArray()[0];
                else id += "_";

                id += DateTime.Today.Year.ToString();
                if (DateTime.Today.Month < 10) id += "0";
                id += DateTime.Today.Month.ToString();
                if (DateTime.Today.Day < 10) id += "0";
                id += DateTime.Today.Day.ToString();

                int count = App.db.Order.Where(x => x.LoginCustomer == App.user.Login).Count();
                while (count > 99)
                {
                    count -= 99;
                }
                if (count < 9) id += "0"; 
                id += (count + 1).ToString(); 

                IdTbx.Text = id;
            }

            DateStartDP.SelectedDate = DateTime.Today;
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteImgButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImageButt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
