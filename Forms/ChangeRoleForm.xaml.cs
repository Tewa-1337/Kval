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
    /// Логика взаимодействия для ChangeRoleForm.xaml
    /// </summary>
    public partial class ChangeRoleForm : Window
    {
        private Users _user = new Users();
        public ChangeRoleForm(Users user)
        {
            InitializeComponent();
            _user = user;
            OfficeComboBox.ItemsSource = DataHelper.GetContext().Offices.ToList();
            DataContext = user;

            if (user.RoleID == 1)
            {
                AdministratorRadio.IsChecked = true;
            }
            else
            {
                UserRadio.IsChecked = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdministratorRadio.IsChecked == true)
            {
                _user.RoleID = 1;
            }
            else
            {
                _user.RoleID = 2;
            }

            DataHelper.GetContext().SaveChanges();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
