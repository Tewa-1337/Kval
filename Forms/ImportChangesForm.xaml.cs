using DigitalSkills2017.Database;
using DigitalSkills2017.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ImportChangesForm.xaml
    /// </summary>
    public partial class ImportChangesForm : Window
    {
        List<ImportChangesHelper> changesWithoutDublicate = new List<ImportChangesHelper>();
        public ImportChangesForm()
        {
            InitializeComponent();
        }

        public List<ImportChangesHelper> ProcessCSV(string path)
        {
            try
            {
                return File.ReadAllLines(path).Where(row => row != null).Select(ImportChangesHelper.ParseRow).ToList();

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Файл занят другим процессом!");
                return null;
            }
        }

        private void ImportChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImportResultHelper.Clear();

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                openFileDialog.ShowDialog();
                var changes = ProcessCSV(openFileDialog.FileName);
                changesWithoutDublicate = changes.Distinct().ToList();

                foreach (var schedule in changesWithoutDublicate)
                {
                    if (schedule == null || schedule.Result != "OK") continue;

                    Schedules schedules = new Schedules()
                    {
                        AircraftID = schedule.AircraftID,
                        Confirmed = true,
                        Date = schedule.Date,
                        Time = schedule.Time,
                        EconomyPrice = schedule.BasePrice,
                        RouteID = DataHelper.GetContext().Routes.Where(n => n.Airports.IATACode == schedule.From
                                                                     && n.Airports1.IATACode == schedule.To).Select(n => n.ID).First(),
                        FlightNumber = schedule.FlightNumber.ToString(),
                    };

                    if (DataHelper.GetContext().Schedules.ToList().Contains(schedules))
                    {
                        ImportResultHelper.Dublicate++;
                        continue;
                    }

                    if (schedule.Function == "ADD")
                    {
                        ImportResultHelper.Successful++;
                        DataHelper.GetContext().Schedules.Add(schedules);
                    }

                }
                DataHelper.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SuccessfulLabel.Content = ImportResultHelper.Successful;
            DublicateLabel.Content = ImportResultHelper.Dublicate;
            RecordLabel.Content = ImportResultHelper.MissFields;
        }
    }
}
