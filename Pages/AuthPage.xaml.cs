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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DigitalSkills2017.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        int attemp = 0;
        int startTime = 0;
        int endTime = 10;
        DispatcherTimer timer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0,0,0,1);
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            startTime++;
            TimeLabel.Content = $"До разблокировки осталось {10 - startTime} секунд";
            if (startTime >= endTime)
            {
                this.IsEnabled = true;
                timer.Stop();
                TimeLabel.Content = "";
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginText.Text;
            string password = PasswordText.Text;

            try
            {
                Users user = DataHelper.GetContext().Users.Where(n => n.Email == login && n.Password == password).Single();
                if(user.RoleID == 1)
                    NavigationService.Navigate(new AdministratorPage(user));
                if (user.RoleID == 2)
                {
                    if (user.Active == true)
                        NavigationService.Navigate(new UserPage(user));
                    else
                        MessageBox.Show("Вы заблокированы в системе!");
                }

            }
            catch
            {
                MessageBox.Show("Неправильные логин или пароль!");
                attemp++;
                if (attemp >= 3)
                {
                    MessageBox.Show("Данные были введены неправильно три и более раза подряд. Окно будет заблокировано следующие 10 секунд.");
                    timer.Start();
                    this.IsEnabled = false;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LoginText.Text = "j.doe@amonic.com";
            PasswordText.Text = "123";
        }

        private void UserRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LoginText.Text = "a.hobart@amonic.com";
            PasswordText.Text = "6996";
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoginText.Text = "";
            PasswordText.Text = "";
        }
    }
}
