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
    public class MeasurementRepository : BaseRepository<Measurement>, IMeasurementRepository
    {
        private readonly List<Measurement> MeasurementCache;

        public MeasurementRepository(VehicleDbContext dbContext) : base(dbContext)
        {
            MeasurementCache = new List<Measurement>();
        }

        public async Task<Measurement> GetUniqueByDate(
            int vehicleId, 
            int shopId, 
            int measurementPointId, 
            DateTime date)
        {
            if (MeasurementCache.Exists(
                x => x.VehicleId == vehicleId && 
                x.ShopId == shopId && 
                x.MeasurementPointId == measurementPointId &&
                x.Date == date))
            {
                return MeasurementCache.Find(
                    x => x.VehicleId == vehicleId &&
                    x.ShopId == shopId &&
                    x.MeasurementPointId == measurementPointId &&
                    x.Date == date);
            }
            else
            {
                var measurement = await _dbContext.Measurements.Where(
                    x => x.VehicleId == vehicleId &&
                    x.ShopId == shopId &&
                    x.MeasurementPointId == measurementPointId &&
                    x.Date == date).FirstOrDefaultAsync();

                if (measurement != null)
                {
                    MeasurementCache.Add(measurement);
                }

                return measurement;
            }
        }
    }
}
