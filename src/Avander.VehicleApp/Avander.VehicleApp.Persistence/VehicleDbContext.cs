using Avander.VehicleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Persistence
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MeasurementPoint> MeasurementPoints { get; set; }
    }
}
