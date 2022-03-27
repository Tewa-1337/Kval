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
    /// Логика взаимодействия для ManageSchedulesPage.xaml
    /// </summary>
    public partial class ManageSchedulesPage : Page
    {
        public ManageSchedulesPage(Users user)
        {
            InitializeComponent();
            ReloadPage();
        }

        public void ReloadPage()
        {
            SchedulesData.ItemsSource = DataHelper.GetContext().Schedules.ToList();
            ToAirportCombo.ItemsSource = DataHelper.GetContext().Airports.ToList();
            FromAirportCombo.ItemsSource = DataHelper.GetContext().Airports.ToList();

            ToAirportCombo.SelectedItem = null;
            FromAirportCombo.SelectedItem = null;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var sortSchedules = DataHelper.GetContext().Schedules.ToList();

            var from = (Countries)FromAirportCombo.SelectedItem;
            var to = (Countries)ToAirportCombo.SelectedItem;

            if (ToAirportCombo.SelectedItem == FromAirportCombo.SelectedItem &&
                FromAirportCombo.SelectedItem != null && ToAirportCombo.SelectedItem != null)
            {
                MessageBox.Show("To and From can`t be equal");
                return;
            }

            if(FromAirportCombo.SelectedItem != null)
                sortSchedules = sortSchedules.Where(n => n.Routes.Airports.Countries == from).ToList();
            if (ToAirportCombo.SelectedItem != null)
                sortSchedules = sortSchedules.Where(n => n.Routes.Airports1.Countries == to).ToList();
            if (FlightNumText.Text != "")
                sortSchedules = sortSchedules.Where(n=>n.FlightNumber == FlightNumText.Text).ToList();
            if (OutboundDatePicker.SelectedDate != null)
            {
                var date = ((DateTime)OutboundDatePicker.SelectedDate).Date;
                sortSchedules = sortSchedules.Where(n => n.Date == date).ToList();
            }
            if (SortCombo.SelectedItem != null)
            {
                if(SortCombo.Text == "Price")
                    sortSchedules = sortSchedules.OrderBy(n => n.EconomyPrice).ToList();
                if (SortCombo.Text == "Confirmed")
                    sortSchedules = sortSchedules.OrderBy(n => n.Confirmed).ToList();
                if (SortCombo.Text == "Date")
                    sortSchedules = sortSchedules.OrderBy(n => n.Date).ToList();

            }

            SchedulesData.ItemsSource = sortSchedules;
        }

        private void FlightNumText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void CancelFlightButton_Click(object sender, RoutedEventArgs e)
        {
            if (SchedulesData.SelectedItem is Schedules schedules)
            {
                schedules.Confirmed = schedules.Confirmed == true ? false : true;
                ReloadPage();
            }
            else
            {
                MessageBox.Show("Select the schedule!");
            }
        }

        private void EditFlightButton_Click(object sender, RoutedEventArgs e)
        {
            if (SchedulesData.SelectedItem is Schedules schedules)
            {
                EditScheduleForm editScheduleForm = new EditScheduleForm(schedules);
                editScheduleForm.ShowDialog();
                ReloadPage();
            }
            else
            {
                MessageBox.Show("Select the schedule!");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadPage();
        }

        private void ImportChangesButton_Click(object sender, RoutedEventArgs e)
        {
            ImportChangesForm importChangesForm = new ImportChangesForm();
            importChangesForm.ShowDialog();
            ReloadPage();
        }
    }
}
