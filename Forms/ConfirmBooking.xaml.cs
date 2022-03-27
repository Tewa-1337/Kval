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
    /// Логика взаимодействия для ConfirmBooking.xaml
    /// </summary>
    public partial class ConfirmBooking : Window
    {
        public ConfirmBooking(BookTicketsForm bookTicketsForm)
        {
            InitializeComponent();
            decimal amount = bookTicketsForm.ticketsList.Select(n => n.Schedules.EconomyPrice).Sum();

            AmountLabel.Content = $"Total amount: {Math.Round(amount)} $";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            DataHelper.GetContext().SaveChanges();
            this.Close();
        }
    }
}
