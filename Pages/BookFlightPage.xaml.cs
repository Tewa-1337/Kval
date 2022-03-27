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
    /// Логика взаимодействия для BookFlightPage.xaml
    /// </summary>
    public partial class BookFlightPage : Page
    {
        public Users user { get; set; }
        public BookFlightPage(Users user)
        {
            InitializeComponent();

            ReloadPage();
            this.user = user;
            CabinTypeComboBox.SelectedIndex = 0;

        }

        public void ReloadPage()
        {
            try
            {
                DataHelper.GetContext().ChangeTracker.Entries().ToList().ForEach(n => n.Reload());
            }
            catch
            {

            }

            FromComboBox.ItemsSource = DataHelper.GetContext().Airports.ToList();
            ToComboBox.ItemsSource = DataHelper.GetContext().Airports.ToList();
            CabinTypeComboBox.ItemsSource = DataHelper.GetContext().CabinTypes.ToList();
            OneWayScheduleData.ItemsSource = DataHelper.GetContext().Schedules.ToList();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadPage();
            List<Schedules> FromTemp = new List<Schedules>(DataHelper.GetContext().Schedules.ToList());
            List<Schedules> From = new List<Schedules>();
            List<Schedules> returnList = new List<Schedules>();

            if (CabinTypeComboBox.Text == "Business")
            {
                foreach (var item in FromTemp)
                {
                    item.EconomyPrice = item.EconomyPrice + (item.EconomyPrice * (decimal)0.30);
                    returnList.Add(item);
                    From.Add(item);
                }
            }

            if (CabinTypeComboBox.Text == "Economy")
            {
                From = FromTemp;
                returnList = FromTemp;
            }

            if (CabinTypeComboBox.Text == "First Class")
            {
                foreach (var item in FromTemp)
                {
                    decimal business = item.EconomyPrice + (item.EconomyPrice * (decimal)0.30);
                    item.EconomyPrice = business + (business * (decimal)0.35);
                    returnList.Add(item);
                    From.Add(item);
                }
            }


            if (FromComboBox.SelectedItem is Airports fromAirport)
            {
                From = From.Where(n => n.Routes.Airports.ID == fromAirport.ID).ToList();
                returnList = returnList.Where(n => n.Routes.Airports1.ID == fromAirport.ID).ToList();
            }
            if (ToComboBox.SelectedItem is Airports toAirport)
            {
                returnList = returnList.Where(n => n.Routes.Airports.ID == toAirport.ID).ToList();
                From = From.Where(n => n.Routes.Airports1.ID == toAirport.ID).ToList();
            }

            if (OutboundDatePicker.SelectedDate is DateTime outboundDate)
            {
                if (ThreeDayCheck.IsChecked == true)
                {
                    var afterDate = outboundDate + new TimeSpan(3, 0, 0, 0);
                    var beforeDate = outboundDate - new TimeSpan(3, 0, 0, 0);

                    From = From.Where(n => n.Date <= afterDate && n.Date >= beforeDate).ToList();
                }
                else
                {
                    From = From.Where(n => n.Date == outboundDate).ToList();
                }
            }

            if (ReturnDatePicker.SelectedDate is DateTime returnDate && ReturnRadio.IsChecked == true)
            {
                if (ThreeDayCheck.IsChecked == true)
                {
                    var afterDate = returnDate + new TimeSpan(3, 0, 0, 0);
                    var beforeDate = returnDate - new TimeSpan(3, 0, 0, 0);

                    returnList = returnList.Where(n => n.Date <= afterDate && n.Date >= beforeDate).ToList();
                }
                else
                {
                    returnList = returnList.Where(n => n.Date == returnDate).ToList();
                }
            }

            foreach (var item in From)
            {
             /*   item.NumberOfStops = 0;
                if (item.Routes.Airports.IATACode == "DOH" && item.Routes.Airports1.IATACode == "AUH")
                    item.NumberOfStops++;
                if (item.Routes.Airports.IATACode == "AUH" && item.Routes.Airports1.IATACode == "DOH")
                    item.NumberOfStops++;
                if (item.Routes.Airports.IATACode == "AUH" && item.Routes.Airports1.IATACode == "CAI")
                    item.NumberOfStops++;
                if (item.Routes.Airports.IATACode == "CAI" && item.Routes.Airports1.IATACode == "AUH")
                    item.NumberOfStops++;*/

            }

            OneWayScheduleData.ItemsSource = From;
            if (ReturnRadio.IsChecked == true)
                ReturnScheduleData.ItemsSource = returnList;

        }

        private void ReturnRadio_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void OneWayRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnRadio_Unchecked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).IsChecked == true)
            {
                ReturnScheduleData.ItemsSource = DataHelper.GetContext().Schedules.ToList();
            }
            else
            {
                ReturnScheduleData.ItemsSource = new List<Schedules>();
            }
        }

        private void ThreeDayCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BookFlightButton_Click(object sender, RoutedEventArgs e)
        {
            if (OneWayScheduleData.SelectedItem == null &&
                ReturnScheduleData.SelectedItem == null && ReturnRadio.IsChecked == false)
            {
                MessageBox.Show("Select outbound to continue!");
                return;
            }

            Schedules outboundSchedule = (Schedules)OneWayScheduleData.SelectedItem;
            Schedules returnSchedule = (Schedules)ReturnScheduleData.SelectedItem;

            if (long.Parse(PassengerText.Text) > DataHelper.GetContext().Tickets.Where(n => n.Schedules.ID == outboundSchedule.ID).Count())
            {
                MessageBox.Show("Schedule has not seats for this passenger!");
                return;
            }

            BookTicketsForm bookTicketsForm = new BookTicketsForm(outboundSchedule, returnSchedule, this);
            bookTicketsForm.ShowDialog();
        }

        private void PassengerText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
