using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Domain.Entities
{
    public class MeasurementPoint
    {
        public int MeasurementPointId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
