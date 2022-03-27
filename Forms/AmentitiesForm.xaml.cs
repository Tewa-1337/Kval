using DigitalSkills2017.Database;
using DigitalSkills2017.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AmentitiesForm.xaml
    /// </summary>
    public partial class AmentitiesForm : Window
    {
        private Tickets _ticket = new Tickets();
        private decimal _sum = 0;
        private List<Amenities> _amenities = new List<Amenities>(DataHelper.GetContext().Amenities.ToList());
        public AmentitiesForm()
        {
            InitializeComponent();
            AmentitiesList.ItemsSource = _amenities;
        }

        private void CancelChanges()
        {
            foreach(var check in DataHelper.GetContext().Amenities.ToList())
            {
              //  check.Checked = false;
            }

            foreach (var entry in DataHelper.GetContext().ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; 
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        private void SelectBookButton_Click(object sender, RoutedEventArgs e)
        {
            SchedulesComboBox.ItemsSource = DataHelper.GetContext().Tickets.Where(n => n.BookingReference == BookText.Text).Select(n => n.Schedules).ToList();
            _ticket = DataHelper.GetContext().Tickets.Where(n => n.BookingReference == BookText.Text).FirstOrDefault();
            DataContext = _ticket;

            List<AmenitiesTickets> amenitiesTickets = new List<AmenitiesTickets>();
            List<Amenities> amenities = new List<Amenities>();
            try
            {

                amenitiesTickets = new List<AmenitiesTickets>(DataHelper.GetContext().AmenitiesTickets.Where(n => n.TicketID == _ticket.ID).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данного билета не существует!");
                return;
            }

            foreach (var amenitie in _amenities)
            {
                AmenitiesTickets amenitiesTicket = new AmenitiesTickets { AmenityID = amenitie.ID };

                if (amenitiesTickets.Contains(amenitiesTicket))
                {
                //    amenitie.Checked = true;
                }
                amenities.Add(amenitie);
            }
            AmentitiesList.ItemsSource = amenities;

            
          //  ResItemsSelected.Text = $"Items selected: {amenities.Where(n => n.Checked == true).Count()}";
        }

        private void SchedulesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string amentitieName = ((CheckBox)sender).Content.ToString();
            var amentitie = DataHelper.GetContext().Amenities.Where(n => n.Service == amentitieName).Single();
            var ticket = _ticket;

            _sum += amentitie.Price;
            ResTotalPayable.Text = $"Total payable: {Math.Round(_sum)}";
            ResDutiesAndTaxes.Text = $"Duties and taxes: {double.Parse(_sum.ToString())*0.05}";

            AmenitiesTickets amenitiesTickets = new AmenitiesTickets
            {
                AmenityID = amentitie.ID,
                TicketID = ticket.ID,
                Price = amentitie.Price
            };

            DataHelper.GetContext().AmenitiesTickets.Add(amenitiesTickets);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string amentitieName = ((CheckBox)sender).Content.ToString();
            var amentitie = DataHelper.GetContext().Amenities.Where(n => n.Service == amentitieName).Single();

            _sum -= amentitie.Price;
            ResTotalPayable.Text = $"Total payable: {Math.Round(_sum)}";
            ResDutiesAndTaxes.Text = $"Duties and taxes: {double.Parse(_sum.ToString()) * 0.05}";

            AmenitiesTickets amenitiesTickets = DataHelper.GetContext().AmenitiesTickets.Where(n => n.AmenityID == amentitie.ID && n.TicketID == _ticket.ID && n.Price == amentitie.Price).FirstOrDefault();

            try
            {
                DataHelper.GetContext().AmenitiesTickets.Remove(amenitiesTickets);
            }
            catch
            {

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DataHelper.GetContext().SaveChanges();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CancelChanges();
        }
    }
}
