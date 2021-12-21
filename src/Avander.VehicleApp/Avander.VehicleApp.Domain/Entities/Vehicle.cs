using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [MaxLength(15)]
        public string JSN { get; set; }

        [MaxLength(50)]
        public string VehicleModel { get; set; }
    }
}
