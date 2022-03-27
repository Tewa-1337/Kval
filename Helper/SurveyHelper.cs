using DigitalSkills2017.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSkills2017.Helper
{
    public class SurveyHelper
    {
        private static  Session4Entities _context = new Session4Entities();
        public static Session4Entities GetContext()
        {
            return _context;
        }
    }
}
