using DigitalSkills2017.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSkills2017.Helper
{
    public class DataHelper
    {
        private static airEntities _context = new airEntities();

        public static airEntities GetContext()
        {
            return _context;
        }
    }
}
