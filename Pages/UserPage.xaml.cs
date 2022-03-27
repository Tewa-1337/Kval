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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _user = new Users();
        public UserPage(Users user)
        {
            InitializeComponent();
            this._user = user;
            TrackingHelper.track = new Tracking
            {
                UserID = user.ID,
                LoginTime = DateTime.Now,
                Crashe = true
            };

            var crashe = user.Tracking.Where(n=>n.Crashe == true && n.CrasheReason == null).FirstOrDefault();

            if (crashe != null)
            {
                CrasheReportForm crasheReportForm = new CrasheReportForm(crashe);
                crasheReportForm.ShowDialog();
            }

            WelcomeLabel.Content = $"Hi {user.FirstName}, welcome to AMONIC Airlines";
            TimeSpentLabel.Content = $"Time spent on system: {user.Tracking.Select(n=>n.OnSystemTime).Sum()/60}";
            CrashesNumberLabel.Content = $"Number of crashes: {user.Tracking.Where(n=>n.Crashe == true).Count()}";

            TrackingData.ItemsSource = DataHelper.GetContext().Tracking.Where(n=>n.UserID == user.ID).ToList();
            
            DataHelper.GetContext().Tracking.Add(TrackingHelper.track);
            DataHelper.GetContext().SaveChanges();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == false)
            {
                TrackingHelper.SaveTrack();
            }
        }

        private void BookFlightButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookFlightPage(_user));
        }

        private void Amentities_Click(object sender, RoutedEventArgs e)
        {
            AmentitiesForm amentitiesForm = new AmentitiesForm();
            amentitiesForm.ShowDialog();
        }

        private void ReportAmentities_Click(object sender, RoutedEventArgs e)
        {
            ReportAmenitiesForm reportAmenitiesForm = new ReportAmenitiesForm();
            reportAmenitiesForm.ShowDialog();
        }
    }
}
