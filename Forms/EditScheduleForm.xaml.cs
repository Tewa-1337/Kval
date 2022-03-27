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
    /// Логика взаимодействия для EditScheduleForm.xaml
    /// </summary>
    public partial class EditScheduleForm : Window
    {
        private Schedules _schedules = new Schedules();
        public EditScheduleForm(Schedules schedules)
        {
            InitializeComponent();
            DataContext = schedules;
        }
        
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ScheduleDatePicker.SelectedDate == null)
                errors.AppendLine("Введите корректную дату!");
            try
            {
                string[] time = TimeText.Text.Split(new char[] { ':' });
                _schedules.Time = new TimeSpan(int.Parse(time[0]), int.Parse(time[2]), 0);
            }
            catch
            {
                errors.AppendLine("Введите корректное время!");
            }

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            DataHelper.GetContext().SaveChanges();
            this.Close();
        }

        private void TimeText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789:".IndexOf(e.Text) < 0;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
