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

        public int GetTotalCountForFilter(
            string jsn = "",
            string measurementPoint = "",
            int? shop = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var query = _dbContext.Measurements.AsQueryable();

            if (!string.IsNullOrWhiteSpace(jsn))
            {
                query = query.Where(x => x.Vehicle.JSN == jsn);
            }
            if (!string.IsNullOrWhiteSpace(measurementPoint))
            {
                query = query.Where(x => x.MeasurementPoint.Name == measurementPoint);
            }
            if (shop.HasValue)
            {
                query = query.Where(x => x.ShopId == shop.Value);
            }
            if (fromDate.HasValue)
            {
                query = query.Where(x => x.Date >= fromDate);
            }
            if (toDate.HasValue)
            {
                query = query.Where(x => x.Date <= toDate);
            }

            return query.Count();
        }

        public async Task<List<Measurement>> GetByFilterPaged(
            bool includeParents = false,
            int page = 1, 
            int size = 50, 
            string jsn = "", 
            string measurementPoint = "", 
            int? shop = null, 
            DateTime? fromDate = null, 
            DateTime? toDate = null)
        {
            var query = _dbContext.Measurements.AsQueryable();

            if (includeParents)
            {
                query = query
                    .Include(x => x.MeasurementPoint)
                    .Include(x => x.Vehicle)
                    .Include(x => x.Shop);
            }

            if (!string.IsNullOrWhiteSpace(jsn))
            {
                query = query.Where(x => x.Vehicle.JSN == jsn);
            }
            if (!string.IsNullOrWhiteSpace(measurementPoint))
            {
                query = query.Where(x => x.MeasurementPoint.Name == measurementPoint);
            }
            if (shop.HasValue)
            {
                query = query.Where(x => x.ShopId == shop.Value);
            }
            if (fromDate.HasValue)
            {
                query = query.Where(x => x.Date >= fromDate);
            }
            if (toDate.HasValue)
            {
                query = query.Where(x => x.Date <= toDate);
            }

            var orderedQuery = query
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(x => x.Id);

            return await orderedQuery.ToListAsync();
        }
    }
}
