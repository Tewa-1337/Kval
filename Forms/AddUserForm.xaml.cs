using DigitalSkills2017.Database;
using DigitalSkills2017.Helper;
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
using System.Windows.Shapes;

namespace DigitalSkills2017.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddUserForm.xaml
    /// </summary>
    public partial class AddUserForm : Window
    {
        private Users _user = new Users();
        public AddUserForm()
        {
            InitializeComponent();

            OfficesComboBox.ItemsSource = DataHelper.GetContext().Offices.ToList();

            DataContext = _user;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_user.Email))
            {
                errors.AppendLine("Введите Email.");
            }
            else
            {
                string email = _user.Email;
                if (DataHelper.GetContext().Users.Select(n=>n.Email).ToList().Contains(email))
                {
                    errors.AppendLine("Такой Email уже существует!");
                }
            }

            if (string.IsNullOrWhiteSpace(_user.FirstName))
                errors.AppendLine("Введите First name.");
            if (string.IsNullOrWhiteSpace(_user.LastName))
                errors.AppendLine("Введите Last name.");
            if (string.IsNullOrWhiteSpace(_user.Password))
                errors.AppendLine("Введите Password.");
            if (BirthDatePicker.SelectedDate == null)
                errors.AppendLine("Выберите дату рождения.");
            if (OfficesComboBox.SelectedItem == null)
                errors.AppendLine("Выберите Office.");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _user.Active = true;
                _user.RoleID = 2;

                DataHelper.GetContext().Users.Add(_user);
                DataHelper.GetContext().SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
