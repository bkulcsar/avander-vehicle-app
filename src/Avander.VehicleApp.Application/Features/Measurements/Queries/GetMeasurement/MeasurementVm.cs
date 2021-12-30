﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Application.Features.Measurements.Queries.GetMeasurement
{
    public class MeasurementVm
    {
        public int Id { get; set; }
        public decimal? Gap { get; set; }
        public decimal? Flush { get; set; }
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public int MeasurementPointId { get; set; }
        public int ShopId { get; set; }
    }
}
