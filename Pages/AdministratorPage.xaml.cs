using DigitalSkills2017.Database;
using DigitalSkills2017.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalSkills2017.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        Users _user = new Users();
        public AdministratorPage(Users user)
        {
            InitializeComponent();
            _user = user;
            ReloadPage();
        }

        public void ReloadPage()
        {
            DataHelper.GetContext().ChangeTracker.Entries().ToList().ForEach(n => n.Reload());
            UsersData.ItemsSource = DataHelper.GetContext().Users.ToList();
            OfficeComboBox.ItemsSource = DataHelper.GetContext().Offices.ToList();

        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void AddUserItem_Click(object sender, RoutedEventArgs e)
        {
            AddUserForm form = new AddUserForm();
            form.ShowDialog();
            ReloadPage();
        }

        private void EnableDisableButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersData.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }

            if (UsersData.SelectedItem is Users user)
            {
                if (user.Active == true)
                    user.Active = false;
                else
                    user.Active = true;

                DataHelper.GetContext().SaveChanges();
                ReloadPage();
            }
        }

        private void OfficeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (OfficeComboBox.SelectedItem is Offices office)
            {
                UsersData.ItemsSource = DataHelper.GetContext().Users.Where(n => n.OfficeID == office.ID).ToList();
            }
            else
            {
                ReloadPage();
            }

        }

        private void OfficeComboBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OfficeComboBox.SelectedItem != null)
            {
                OfficeComboBox.SelectedItem = null;
                ReloadPage();
            }
        }

        private void ChangeRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersData.SelectedItem is Users user)
            {
                ChangeRoleForm form = new ChangeRoleForm(user);
                form.ShowDialog();
                ReloadPage();
            }
        }

        private void SchedulesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageSchedulesPage(_user));
        }

        private void BookFlightButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookFlightPage(_user));
        }

        private void SurveyButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.SurveyResult surveyResult = new Forms.SurveyResult();
            surveyResult.ShowDialog();
        }
    }
}
