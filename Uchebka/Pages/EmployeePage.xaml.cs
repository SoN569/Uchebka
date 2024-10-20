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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public User employee = new User();
        public EmployeePage(User _user)
        {
            InitializeComponent();
            employee = _user;
            OperationCbx.ItemsSource = App.db.Operation.ToList();
            OperationCbx.DisplayMemberPath = "Name";

            List<Role> roles = App.db.Role.ToList();
            roles.RemoveAt(3);
            RoleCbx.ItemsSource = roles;
            RoleCbx.DisplayMemberPath = "Name";

            if (employee.Login != null)
            {
                SurnameTbx.Text = employee.LastName;
                NameTbx.Text = employee.FirstName;
                PatronymicTbx.Text = employee.Patronymic;
                BirthDP.Text = employee.DateBithday.ToString();
                EducationTbx.Text = employee.Education;
                PostTbx.Text = employee.Post;
                RoleCbx.SelectedIndex = (int)employee.RoleId - 1;
                LoginTbx.Text = employee.Login;
                PasswordPbx.Password = employee.Password;
                LoginSP.Visibility = Visibility.Collapsed;
                PasswordSP.Visibility = Visibility.Collapsed;

                foreach(UserOperation usop in App.db.UserOperation.Where(x => x.Login == employee.Login).ToList())
                {
                    Operation op = App.db.Operation.First(x => x.Id == usop.IdOperation);
                    OperationsLV.Items.Add(op);
                }
            }
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            if (NameTbx.Text == "" || SurnameTbx.Text == "" || LoginTbx.Text == "" || PasswordPbx.Password == "" || RoleCbx.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не заполнили все обязательные поля!", "Ошибка редактирования");
            }
            else
            {
                employee.LastName = SurnameTbx.Text;
                employee.FirstName = NameTbx.Text;
                employee.RoleId = RoleCbx.SelectedIndex + 1;
                if (PatronymicTbx.Text != "") employee.Patronymic = PatronymicTbx.Text;
                if (BirthDP.SelectedDate != null) employee.DateBithday = BirthDP.SelectedDate;
                if (EducationTbx.Text != "") employee.Education = EducationTbx.Text;
                if (PostTbx.Text != "") employee.Post = PostTbx.Text;
                if (employee.Login == null)
                {
                    employee.Login = LoginTbx.Text;
                    employee.Password = PasswordPbx.Password;
                    App.db.User.Add(employee);
                }

                foreach (Operation op in OperationsLV.Items)
                {
                    UserOperation usop = new UserOperation();
                    usop.IdOperation = op.Id;
                    usop.Login = employee.Login;

                    if (App.db.UserOperation.Where(x => x.IdOperation == usop.IdOperation && x.Login == usop.Login).Count() > 0) ;
                    else App.db.UserOperation.Add(usop);
                }

                App.db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранились!", "Успешное редактирование");
                NavigationService.Navigate(new EmployeeListPage());
            }
        }

        private void AddOpButt_Click(object sender, RoutedEventArgs e)
        {
            if (OperationCbx.SelectedIndex >= 0)
            {
                if (OperationsLV.Items.Contains(OperationCbx.SelectedItem)) MessageBox.Show("Вы уже добавили эту операцию!", "Ошибка добавления операции");
                else
                {
                    OperationsLV.Items.Add(OperationCbx.SelectedItem);
                    MessageBox.Show("Операция успешно добавлена!", "Успешное добавление операции");
                }
            }
            else MessageBox.Show("Операция не выбрана!", "Ошибка добавления операции");
        }

        private void DelOpButt_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsLV.SelectedItem == null) MessageBox.Show("Не выбрана операция для удаления", "Ошибка удаления операции");
            else
            {
                Operation op = (Operation)OperationsLV.SelectedItem;
                if (App.db.UserOperation.Where(x => x.IdOperation == op.Id && x.Login == employee.Login).Count() > 0)
                {
                    UserOperation delUsOp = App.db.UserOperation.First(x => x.IdOperation == op.Id && x.Login == employee.Login);
                    App.db.UserOperation.Remove(delUsOp);
                }
                OperationsLV.Items.Remove(OperationsLV.SelectedItem);
            }
        }
    }
}
