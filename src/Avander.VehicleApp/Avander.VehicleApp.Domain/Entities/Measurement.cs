using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Domain.Entities
{
    public class Measurement
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int ShopId { get; set; }

        public int MeasurementPointId { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Gap { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Flush { get; set; }
    }
}
