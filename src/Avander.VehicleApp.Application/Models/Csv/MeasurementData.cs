using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Models.Csv
{
    internal class MeasurementData
    {
        public string MeasurementPointName { get; set; }
        public string JSN { get; set; }
        public string VehicleModel { get; set; }
        public string ShopName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Flush { get; set; }
        public string Gap { get; set; }

        public DateTime GetDateTime()
        {
            return new DateTime(
                Date.Year,
                Date.Month,
                Date.Day,
                Time.Hours,
                Time.Minutes,
                Time.Seconds);
        }
    }
}
