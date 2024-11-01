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
    /// Логика взаимодействия для EditQualityPage.xaml
    /// </summary>
    public partial class EditQualityPage : Page
    {
        public Quality quality;
        public EditQualityPage(Quality _qual)
        {
            quality = _qual;
            InitializeComponent();
            ParametrCbx.ItemsSource = App.db.QualityParametr.ToList();
            ParametrCbx.DisplayMemberPath = "Name";

            OrderCbx.ItemsSource = App.db.Order.Where(x => x.IdStatus == 7).ToList();
            OrderCbx.DisplayMemberPath = "Id";

            if (quality.Id > 0)
            {
                ParametrCbx.SelectedIndex = (int)(quality.IdParametr - 1);
                OrderCbx.Text = quality.IdOrder;
                CommentTbx.Text = quality.Comment;
                ExaminationCbx.Text = quality.Examination;
            }
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ParametrCbx.SelectedItem == null || OrderCbx.SelectedItem == null || ExaminationCbx.SelectedItem == null)
                {
                    MessageBox.Show("Не все обязательные поля заполнены!", "Ошибка сохранения");
                }
                else
                {
                    quality.IdParametr = ParametrCbx.SelectedIndex + 1;
                    quality.Comment = CommentTbx.Text;
                    quality.IdOrder = OrderCbx.Text;
                    quality.Examination = ExaminationCbx.Text;
                    if (quality.Id < 1)
                    {
                        if (App.db.Quality.Where(x => x.IdParametr == quality.IdParametr && x.IdOrder == quality.IdOrder).Count() > 0)
                        {
                            MessageBox.Show("Уже существует оценка этого заказа по данному параметру!", "Ошибка сохранения");
                        }
                        else
                        App.db.Quality.Add(quality);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Оценка была успешно сохранена!", "Успешное сохранение");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при сохранении оценки качества!", "Ошибка сохранения");
            }
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QualityPage());
        }

        private void ExaminationCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExaminationCbx.SelectedIndex == 0)
            {
                CommentTbx.IsEnabled = true;
            }
            else
            {
                CommentTbx.IsEnabled = false;
                CommentTbx.Text = "";
            }
        }
    }
}
