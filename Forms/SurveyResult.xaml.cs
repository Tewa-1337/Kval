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
    /// Логика взаимодействия для SurveyResult.xaml
    /// </summary>
    public partial class SurveyResult : Window
    {
        public SurveyResult()
        {
            InitializeComponent();
            SurveyFrame.Navigate(new SurveyPage());
        }
    }
}
