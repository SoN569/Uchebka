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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
                    CostTbx.IsReadOnly = true;
                    DateEndDP.IsEnabled = false;
                    break;

                case 4:
                    CustomerSP.Visibility = Visibility.Collapsed;
                    CostTbx.IsReadOnly = true;
                    DateEndDP.IsEnabled = false;
                    break;

                case 2:
                    NameTbx.IsEnabled = false;
                    CustomerCbx.IsReadOnly = true;
                    DateStartDP.IsEnabled = false;
                    DescriptionTbx.IsEnabled = false;
                    break;
            }

            CustomerCbx.ItemsSource = App.db.User.Where(x => x.RoleId == 4).ToList();
            CustomerCbx.DisplayMemberPath = "LastName";
            UnitCbx.ItemsSource = App.db.Unit.ToList();
            UnitCbx.DisplayMemberPath = "Name";

            if (order.Id == null)
            {
                DateStartDP.SelectedDate = DateTime.Today;
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
                string numId = "";
                if (count < 9) numId += "0";
                numId += (count + 1).ToString();


                while (App.db.Order.Where(x => x.LoginCustomer == App.user.Login && x.Id.EndsWith(numId)).Count() > 0)
                {
                    count++;
                    numId = "";
                    if (count < 9) numId += "0"; 
                    numId += (count + 1).ToString(); 
                }

                IdTbx.Text = id + numId;
            }
            else
            {
                IdTbx.Text = order.Id;
                NameTbx.Text = order.Name;
                if (App.user.RoleId == 5)
                {
                    CustomerCbx.Text = App.db.User.First(x => x.Login == order.LoginCustomer).LastName;
                }
                if (App.user.RoleId == 2)
                {
                    CostTbx.Text = order.Price.ToString();
                    DateEndDP.SelectedDate = order.DateEnd.Value.Date;
                }    
                
                DateStartDP.SelectedDate = order.DateOrder.Date;
                DescriptionTbx.Text = order.Description;
                if (App.db.DocOrder.Where(x => x.IdOrder == order.Id).Count() > 0)
                {
                    DocOrder doc = App.db.DocOrder.First(x => x.IdOrder == order.Id);
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = new MemoryStream(doc.Photo);
                    imageSource.EndInit();
                    DocImg.Source = imageSource;
                }
                

                if (App.db.Size.Where(x => x.IdOrder == order.Id).Count() > 0)
                {
                    foreach(Components.Size size in App.db.Size.Where(x => x.IdOrder == order.Id).ToList())
                    {
                        SizeLV.Items.Add(size);
                    }
                }
            }
           
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            
            if (App.user.RoleId == 4)
            {
                if (NameTbx.Text == "")
                {
                    MessageBox.Show("Должно быть указано наименование заказа!", "Ошибка сохранения");
                }
                order.LoginCustomer = App.user.Login;
                order.Name = NameTbx.Text;
                order.IdStatus = 1;
                order.DateOrder = DateStartDP.SelectedDate.Value.Date;
                order.Description = DescriptionTbx.Text;
                if (order.Id == null)
                {
                    order.Id = IdTbx.Text;
                    App.db.Order.Add(order);
                }
                order.Id = IdTbx.Text;

                foreach (var size in SizeLV.Items)
                {
                    Components.Size ssss = size as Components.Size;
                    
                    if (App.db.Size.Where(x => x.Name == ssss.Name).Count() == 0)
                    {
                        ssss.IdOrder = order.Id;
                        App.db.Size.Add(ssss);
                    }
                }
            }
            else if (App.user.RoleId == 5)
            {
                order.LoginManager = App.user.Login;
                if (CustomerCbx.SelectedIndex > -1)
                {
                    User us = CustomerCbx.SelectedItem as User;
                    order.LoginCustomer = us.Login;
                }
                else
                {
                    MessageBox.Show("Не выбран заказчик!", "Ошибка сохранения");
                    error = true;
                }
                if (order.Id == null)
                {
                    order.Id = IdTbx.Text;
                    App.db.Order.Add(order);
                }
                order.Id = IdTbx.Text;
                order.Name = NameTbx.Text;
                order.DateOrder = DateStartDP.SelectedDate.Value.Date;
                order.Description = DescriptionTbx.Text;
                order.IdStatus = 3;

                foreach (var size in SizeLV.Items)
                {
                    Components.Size ssss = size as Components.Size;

                    if (App.db.Size.Where(x => x.Name == ssss.Name).Count() == 0)
                    {
                        ssss.IdOrder = order.Id;
                        App.db.Size.Add(ssss);
                    }
                }
            }
            else if (App.user.RoleId == 2)
            {
                if (CostTbx.Text == "" || DateEndDP.Text == "")
                {
                    MessageBox.Show("Не выбран заказчик!", "Ошибка сохранения");
                    error = true;
                }
                
                try
                { 
                    order.Price = decimal.Parse(CostTbx.Text);
                    if (order.Price <= 0)
                    {
                        MessageBox.Show("Некорректная стоимость заказа!", "Ошибка сохранения");
                        error = true;
                    }
                    order.DateEnd = DateEndDP.SelectedDate.Value.Date;
                    if (DateEndDP.SelectedDate.Value <= DateTime.Today)
                    {
                        MessageBox.Show("Некорректная плановая дата!", "Ошибка сохранения");
                        error = true;
                    }
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка сохранения", "Ошибка сохранения");
                    error = true;
                }
            }
            if (error == false)
            {
                App.db.SaveChanges();
                MessageBox.Show("Вы успешно сохранили заказ!", "Успешное сохранение");
            }
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void DeleteImgButt_Click(object sender, RoutedEventArgs e)
        {
            if (App.user.RoleId == 2) MessageBox.Show("Конструктор не имеет может совершать это действие!", "Ошибка удаления");
            else
            {
                //if (App.db.DocOrder.ToList().Count() != 0)
                //{
                //    BitmapImage imageSource = new BitmapImage();
                //    imageSource.BeginInit();
                //    imageSource.StreamSource = new MemoryStream(material.MatImage);
                //    imageSource.EndInit();
                //    MaterialImg.Source = imageSource;
                //}
                //App.db.DocOrder.Remove()
            }
        }

        private void ImageButt_Click(object sender, RoutedEventArgs e)
        {
            if (App.user.RoleId == 2) MessageBox.Show("Конструктор не имеет может совершать это действие!", "Ошибка добавления");
            else
            {
                OpenFileDialog openFile = new OpenFileDialog()
                {
                    Filter = "*.png|*.png|*.jpg|*.jpg|*.jepg|*.jepg"
                };
                if (openFile.ShowDialog().GetValueOrDefault())
                {
                    //order.MatImage = File.ReadAllBytes(openFile.FileName);
                    App.db.DocOrder.Add(new DocOrder()
                    {
                        Photo = File.ReadAllBytes(openFile.FileName),
                        IdOrder = IdTbx.Text
                    });
                    DocImg.Source = new BitmapImage(new Uri(openFile.FileName));

                }
                else MessageBox.Show("Изображение не было выбрано!", "Ошибка изображения");
            }
        }

        private void AddSizeButt_Click(object sender, RoutedEventArgs e)
        {
            if (App.user.RoleId == 2) MessageBox.Show("Конструктор не имеет может совершать это действие!", "Ошибка добавления");
            else
            {
                if (SizeNameTbx.Text == "" || SizeValueTbx.Text == "" || UnitCbx.SelectedIndex == -1)
                {
                    MessageBox.Show("", "");
                }
                else
                {
                    try
                    {
                        if (int.Parse(SizeValueTbx.Text) <= 0)
                            MessageBox.Show("", "");
                        else
                        {
                            Components.Size si = new Components.Size()
                            {
                                Name = SizeNameTbx.Text,
                                SizeValue = decimal.Parse(SizeValueTbx.Text),
                                IdUnit = UnitCbx.SelectedIndex + 1
                            };
                            SizeLV.Items.Add(si);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("", "");
                    }
                }
            }

        }

        private void DelSizeButt_Click(object sender, RoutedEventArgs e)
        {
            if (App.user.RoleId == 2) MessageBox.Show("Конструктор не имеет может совершать это действие!", "Ошибка удаления");
            else
            {
                Components.Size sel = SizeLV.SelectedItem as Components.Size;
                if (sel == null)
                {

                }
                else
                {
                    if (order.Id != null)
                    {
                        //Components.Size s = App.db.Size.First(x => x.Name == sel.Name && x.SizeValue == sel.SizeValue && x.IdOrder == sel.IdOrder);
                        App.db.Size.Remove(sel);
                    }
                    SizeLV.Items.Remove(sel);
                    App.db.SaveChanges();
                }
            }

        }

        private void AfterButt_Click(object sender, RoutedEventArgs e)
        {
            if (App.db.DocOrder.Where(x => x.IdOrder == order.Id).Count() > 1)
            {

            }
        }
    }
}
