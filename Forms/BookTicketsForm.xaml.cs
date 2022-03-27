using DigitalSkills2017.Database;
using DigitalSkills2017.Helper;
using DigitalSkills2017.Pages;
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
    /// Логика взаимодействия для BookTicketsForm.xaml
    /// </summary>
    public partial class BookTicketsForm : Window
    {
        Tickets outboundTickets = new Tickets();
        Tickets returnTickets = new Tickets();



        public List<Tickets> ticketsList = new List<Tickets>();
        public BookTicketsForm(Schedules outboundShedule, Schedules returnSchedule, BookFlightPage page)
        {
            InitializeComponent();

            outboundTickets.Confirmed = true;

            outboundTickets.Users = page.user;
            outboundTickets.UserID = page.user.ID;
            outboundTickets.Schedules = outboundShedule;
            outboundTickets.ScheduleID = outboundShedule.ID;
            outboundTickets.CabinTypes = (CabinTypes)page.CabinTypeComboBox.SelectedItem;
            outboundTickets.CabinTypeID = ((CabinTypes)page.CabinTypeComboBox.SelectedItem).ID;

            returnTickets.Schedules = returnSchedule;
            returnTickets.CabinTypes = outboundTickets.CabinTypes;

            outboundScheduleGrid.DataContext = outboundTickets;
            returnScheduleGrid.DataContext = returnTickets;
            ticketsGrid.DataContext = outboundTickets;

            CountryComboBox.ItemsSource = DataHelper.GetContext().Countries.ToList();
        }

        private void AddPassengerButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(outboundTickets.Firstname))
                errors.AppendLine("Enter the firstname!");
            if (string.IsNullOrWhiteSpace(outboundTickets.Firstname))
                errors.AppendLine("Enter the lastname!");
            if (string.IsNullOrWhiteSpace(outboundTickets.Phone))
                errors.AppendLine("Enter the phone!");
            if (CountryComboBox.SelectedItem == null)
                errors.AppendLine("Enter the country!");
            if (PhoneText.Text.Length > 14)
                errors.AppendLine("Invalid phone number!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            outboundTickets.BookingReference = BookRefGen();

            outboundTickets.PassportCountryID = ((Countries)CountryComboBox.SelectedItem).ID;

            Tickets tickets = new Tickets();
        //    tickets = (Tickets)outboundTickets.Clone();

            if (returnTickets.Schedules != null)
            {
              //  var returnTickets = (Tickets)outboundTickets.Clone();
                returnTickets.Schedules = this.returnTickets.Schedules;
                returnTickets.Schedules.ID = this.returnTickets.Schedules.ID;
                ticketsList.Add(returnTickets);
            }

            ticketsList.Add(tickets);
            TicketsData.Items.Add(tickets);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ticket in ticketsList)
            {
                DataHelper.GetContext().Tickets.Add(ticket);

                try
                {
                    ConfirmBooking confirmBooking = new ConfirmBooking(this);
                    confirmBooking.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RemovePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsData.SelectedItem is Tickets removeTicket)
            {

                foreach (var ticket in ticketsList.Where(n => n.Email == removeTicket.Email).ToList())
                {
                    ticketsList.Remove(ticket);
                    TicketsData.Items.Remove(ticket);
                }
            }
        }

        private string BookRefGen()
        {
            string[] array = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
                                        "A", "S", "D", "F", "G", "H", "J", "K", "L", "M",
                                        "N", "B", "V", "C", "X", "Z"};

            string bookRef = "";

            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                bookRef += array[random.Next(0, array.Length - 1)];
            }

            var bookRefTicket = DataHelper.GetContext().Tickets.Where(n => n.BookingReference == bookRef).FirstOrDefault();
            if (bookRefTicket != null)
            {
                return BookRefGen();
            }

            return bookRef;
        }
    }
}