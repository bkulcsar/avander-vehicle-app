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
        public DateTime Time { get; set; }
        public decimal Flush { get; set; }
        public decimal Gap { get; set; }
    }
}
