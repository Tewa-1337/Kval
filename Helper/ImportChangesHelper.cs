using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSkills2017.Helper
{
    public class ImportChangesHelper : IEquatable<ImportChangesHelper>
    {
        public string Function { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int FlightNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int AircraftID { get; set; }
        public decimal BasePrice { get; set; }
        public string Result { get; set; }

        public static ImportChangesHelper ParseRow(string row)
        {
            var columns = row.Split(new char[] { ',' });
            var date = columns[1].Split(new char[] { '-' });
            try
            {
                return new ImportChangesHelper
                {
                    Function = columns[0],
                    Date = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2])),
                    Time = TimeSpan.Parse(columns[2]),
                    FlightNumber = int.Parse(columns[3]),
                    From = columns[4],
                    To = columns[5],
                    AircraftID = int.Parse(columns[6]),
                    BasePrice = int.Parse(columns[7].Split(new char[] { '.' })[0]),
                    Result = columns[8]
                };
            }
            catch
            {
                ImportResultHelper.MissFields++;
                return null;
            }
        }

        public bool Equals(ImportChangesHelper other)
        {
            if (this.Function == other.Function &&
                this.Date == other.Date &&
                this.Time == other.Time &&
                this.FlightNumber == other.FlightNumber &&
                this.From == other.From &&
                this.To == other.To &&
                this.AircraftID == other.AircraftID &&
                this.BasePrice == other.BasePrice &&
                this.Result == other.Result)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {

            return 0;
        }
    }
}
