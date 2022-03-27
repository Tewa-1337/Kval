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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalSkills2017.Pages
{
    /// <summary>
    /// Логика взаимодействия для SurveyPage.xaml
    /// </summary>
    public partial class SurveyPage : Page
    {
        public SurveyPage()
        {
            InitializeComponent();

            List<Survey> survey = SurveyHelper.GetContext().Survey.ToList();

            SurveyResult result = new SurveyResult()
            {
                MaleSum = survey.Where(n=>n.Gender == "M").Count(),
                FemaleSum = survey.Where(n => n.Gender == "F").Count()
            };

            GenderData.Items.Add(result);
        }
    }
}
    


