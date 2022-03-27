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
    /// Логика взаимодействия для ReportAmenitiesForm.xaml
    /// </summary>
    public partial class ReportAmenitiesForm : Window
    {
        public ReportAmenitiesForm()
        {
            InitializeComponent();

            
        }

        private void ReportAmenities(int FlightNumber)
        {
            List<Amenities> amenities = new List<Amenities>(DataHelper.GetContext().Amenities.ToList());

            foreach (var amenitie in amenities)
            {
                Binding binding = new Binding();
                int res = DataHelper.GetContext().AmenitiesTickets.Where(n => n.Tickets.Schedules.ID == FlightNumber && n.Amenities.Service == amenitie.Service).Count();
                binding.Source = res;

                DataGridTextColumn column = new DataGridTextColumn()
                {
                    Header = amenitie.Service,
                    Width = DataGridLength.Auto,
                    Binding = binding
                };

                AmenitiesData.Columns.Add(column);
            }

            AmenitiesData.Items.Add(1);
        }

        private void MakeReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AmenitiesData.Items.Clear();
                AmenitiesData.Columns.Clear();
                ReportAmenities(int.Parse(FlightText.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Такого рейса нет");
            }
        }

        private void FlightText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
