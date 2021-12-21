using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Avander.VehicleApp.Application.Models.Csv
{
    internal class CsvMeasurementMapping : CsvMapping<MeasurementData>
    {
        public CsvMeasurementMapping()
            : base()
        {
            MapProperty(0, x => x.MeasurementPointName);
            MapProperty(1, x => x.JSN);
            MapProperty(6, x => x.Gap);
            MapProperty(7, x => x.Flush);
            MapProperty(11, x => x.Date);
            MapProperty(12, x => x.Time);
            MapProperty(13, x => x.ShopName);
            MapProperty(14, x => x.VehicleModel);
        }
    }
}
