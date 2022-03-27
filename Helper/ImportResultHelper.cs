using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSkills2017.Helper
{
    public class ImportResultHelper
    {
        public static int Successful = 0;
        public static int Dublicate = 0;
        public static int MissFields = 0;

        public static void Clear()
        {
            Successful = 0;
            Dublicate = 0;
            MissFields = 0;
        }
    }
}
