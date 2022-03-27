using DigitalSkills2017.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSkills2017.Helper
{
    public class TrackingHelper
    {
        public static Tracking track { get; set; }

        public static void SaveTrack()
        {
            track.LogoutTime = DateTime.Now;
            track.OnSystemTime = ((TimeSpan)(track.LogoutTime - track.LoginTime)).Seconds;
            track.Crashe = false;
            DataHelper.GetContext().SaveChanges();
        }
    }
}
