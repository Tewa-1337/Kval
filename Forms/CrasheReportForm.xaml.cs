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
    /// Логика взаимодействия для CrasheReportForm.xaml
    /// </summary>
    public partial class CrasheReportForm : Window
    {
        Tracking _tracking = new Tracking();
        public CrasheReportForm(Tracking tracking)
        {
            InitializeComponent();
            _tracking = tracking;
       //     LogoutLabel.Content = $"No logout reason for your last login on {tracking.LoginTime.Date.ToString("dd.MM.yyyy")} at {tracking.LoginTime.ToString("hh:mm")}";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _tracking.CrasheReason = ReasonText.Text;
            DataHelper.GetContext().SaveChanges();
            this.Close();
        }
    }
}
