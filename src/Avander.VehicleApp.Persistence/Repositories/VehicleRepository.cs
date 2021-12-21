using Avander.VehicleApp.Application.Contracts.Persistence;
using Avander.VehicleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Persistence.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly List<Vehicle> VehicleCache;
        
        public VehicleRepository(VehicleDbContext dbContext) : base(dbContext)
        {
            VehicleCache = new List<Vehicle>();
        }

        public async Task<Vehicle> GetByJSNAndVehicleModel(string jsn, string vehicleModel)
        {
            if (VehicleCache.Exists(x => x.JSN == jsn && x.VehicleModel == vehicleModel))
            {
                return VehicleCache.Find(x => x.JSN == jsn && x.VehicleModel == vehicleModel);
            }
            else
            {
                var vehicle =  await _dbContext.Vehicles.Where(x => x.JSN == jsn && x.VehicleModel == vehicleModel)
                    .FirstOrDefaultAsync();

                if (vehicle != null)
                {
                    VehicleCache.Add(vehicle);
                }

                return vehicle;
            }  
        }

        public override async Task<Vehicle> AddAsync(Vehicle entity)
        {
            await base.AddAsync(entity);
            VehicleCache.Add(entity);

            return entity;
        }
    }
}
